using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using Xabbo.Common.Generator.Diagnostics;
using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;

namespace Xabbo.Common.Generator;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class InterceptorAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create([
        DiagnosticDescriptors.InterceptorMustBePartial,
        DiagnosticDescriptors.ClientSpecifierOnMessageHandler,
        DiagnosticDescriptors.InvalidInterceptMethodSignature,
        DiagnosticDescriptors.InvalidMessageHandlerSignature,
        DiagnosticDescriptors.InvalidMessageIdentifier,
        DiagnosticDescriptors.IdentifiersNotSpecified,
        DiagnosticDescriptors.InvalidSingleClientSpecifier,
        DiagnosticDescriptors.NoTargetClientsOnInterceptAttribute,
        DiagnosticDescriptors.EmptyTargetClientOnInterceptsAttribute,
        DiagnosticDescriptors.InterceptHandlerMustNotBeStatic,
    ]);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.RegisterSymbolAction(AnalyzeClass, SymbolKind.NamedType);
        context.RegisterSymbolAction(AnalyzeMethod, SymbolKind.Method);
    }

    private void AnalyzeClass(SymbolAnalysisContext ctx)
    {
        if (ctx.Symbol is not INamedTypeSymbol type)
            return;

        AttributeData?
            extensionAttribute = null,
            interceptAttribute = null;

        foreach (var attr in type.GetAttributes())
        {
            if (AnalysisHelper.IsExtensionAttribute(attr.AttributeClass))
                extensionAttribute = attr;
            else if (AnalysisHelper.IsInterceptAttribute(attr.AttributeClass))
                interceptAttribute = attr;
        }

        if (extensionAttribute is null && interceptAttribute is null)
            return;

        // Add diagnostic if not partial.
        bool isPartial = type
            .DeclaringSyntaxReferences
            .Any(syntax =>
                syntax.GetSyntax() is BaseTypeDeclarationSyntax declaration &&
                declaration.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword)));

        if (!isPartial)
        {
            ctx.ReportDiagnostic(Diagnostic.Create(
                DiagnosticDescriptors.InterceptorMustBePartial,
                type.OriginalDefinition.Locations[0]
            ));
        }

        // Extract target clients.
        Client targetClients = Client.All;

        var interceptsAttribute = type.GetAttributes().FirstOrDefault(
            x => x.AttributeClass?.ToDisplayString() == Constants.InterceptAttributeMetadataName);

        if (interceptsAttribute is not null &&
            interceptsAttribute.ConstructorArguments.Length > 0 &&
            interceptsAttribute.ConstructorArguments[0].Value is int targetClientValue)
        {
            targetClients = ((Client)targetClientValue) & Client.All;
            if (targetClients is Client.None)
            {
                ctx.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.EmptyTargetClientOnInterceptsAttribute,
                    interceptsAttribute.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                ));
            }
        }
    }

    private void AnalyzeMethod(SymbolAnalysisContext ctx)
    {
        if (ctx.Symbol is not IMethodSymbol method ||
            method.ContainingType is not INamedTypeSymbol containingType)
            return;

        AttributeData?
            extensionAttribute = null,
            interceptAttribute = null;

        foreach (var attribute in containingType.GetAttributes())
        {
            if (AnalysisHelper.IsExtensionAttribute(attribute.AttributeClass))
                extensionAttribute = attribute;
            else if (AnalysisHelper.IsInterceptAttribute(attribute.AttributeClass))
                interceptAttribute = attribute;
        }

        if (extensionAttribute is null && interceptAttribute is null)
            return;

        Client targetClients = Client.All;
        if (interceptAttribute?.ConstructorArguments is [ { Value: int clientValue } ])
            targetClients &= (Client)clientValue;

        AttributeData? methodInterceptAttribute = null;
        bool isIntercept = false, isDirectionalIntercept = false;

        foreach (var attribute in method.GetAttributes())
        {
            if (AnalysisHelper.IsDirectionalInterceptAttribute(attribute.AttributeClass))
            {
                isIntercept = true;
                isDirectionalIntercept = true;
                ValidateIdentifierNames(ctx, attribute);
            }
            else if (AnalysisHelper.IsInterceptAttribute(attribute.AttributeClass))
            {
                isIntercept = true;
                methodInterceptAttribute = attribute;
            }
        }

        if (!isIntercept) return;

        if (method.IsStatic)
        {
            ctx.ReportDiagnostic(Diagnostic.Create(
                DiagnosticDescriptors.InterceptHandlerMustNotBeStatic,
                method.Locations[0]
            ));
        }

        if (isDirectionalIntercept)
        {
            if (!AnalysisHelper.IsInterceptHandlerSignature(method))
            {
                ctx.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.InvalidInterceptMethodSignature,
                    method.Locations[0]
                ));
            }

            if (methodInterceptAttribute is not null)
            {
                if (methodInterceptAttribute.ConstructorArguments is [
                    { Value: int clientValue2 }
                ])
                {
                    targetClients &= (Client)clientValue2;
                    if (targetClients == Client.None)
                    {
                        ctx.ReportDiagnostic(Diagnostic.Create(
                            DiagnosticDescriptors.EmptyTargetClientOnInterceptsAttribute,
                            methodInterceptAttribute.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                        ));
                    }
                }
                else
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticDescriptors.NoTargetClientsOnInterceptAttribute,
                        methodInterceptAttribute.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                    ));
                }
            }
        }
        else
        {
            if (!AnalysisHelper.IsMessageHandlerSignature(method) &&
                !AnalysisHelper.IsInterceptMessageHandlerSignature(method) &&
                !AnalysisHelper.IsInterceptMessageHandlerSignature2(method) &&
                !AnalysisHelper.IsModifyMessageHandlerSignature(method))
            {
                ctx.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.InvalidMessageHandlerSignature,
                    method.Locations[0]
                ));
            }

            if (methodInterceptAttribute?.ConstructorArguments is { Length: > 0 })
            {
                ctx.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.ClientSpecifierOnMessageHandler,
                    methodInterceptAttribute.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                ));
            }
        }
    }

    private void ValidateIdentifierNames(SymbolAnalysisContext ctx, AttributeData attr)
    {
        if (attr.ConstructorArguments is not [ { Kind: TypedConstantKind.Array, Values: var identifiers } ])
            return;

        Location[]? attributeParamLocations = null;
        if (attr.ApplicationSyntaxReference?.GetSyntax() is AttributeSyntax attrSyntax)
            attributeParamLocations = attrSyntax.ArgumentList?.Arguments.Select(x => x.GetLocation()).ToArray();

        for (int i = 0; i < identifiers.Length; i++)
        {
            if (identifiers[i].Value is not string name) continue;

            Client client = Client.None;

            int colonIndex = name.IndexOf(':');
            if (colonIndex >= 0)
            {
                string clientIdentifier = name[..colonIndex];
                if (clientIdentifier.Length == 1)
                    client = clientIdentifier[0].ToClient();

                if (client == Client.None)
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticDescriptors.InvalidSingleClientSpecifier,
                        attributeParamLocations is null
                            ? attr.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                            : attributeParamLocations[i],
                        name[..(colonIndex + 1)]
                    ));
                    continue;
                }

                name = name[(colonIndex + 1)..];
            }

            if (!Identifier.IsValid(name))
            {
                ctx.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticDescriptors.InvalidMessageIdentifier,
                    attributeParamLocations is null
                        ? attr.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                        : attributeParamLocations[i],
                    name
                ));
            }
        }
    }
}