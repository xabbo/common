using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Xabbo.Common.Generator.Diagnostics;
using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;

namespace Xabbo.Common.Generator;

internal static partial class Extractor
{
    internal static class Interceptor
    {
        static Direction GetDirectionForInterceptAttribute(string name) => name switch
        {
            Constants.InterceptInAttributeMetadataName => Direction.In,
            Constants.InterceptOutAttributeMetadataName => Direction.Out,
            _ => Direction.None,
        };

        internal static Result<InterceptorInfo?> ExtractInfo(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (context.SemanticModel.GetDeclaredSymbol(context.TargetNode) is not INamedTypeSymbol @class)
                return null;

            ImmutableArray<ISymbol> members = @class.GetMembers();

            List<InterceptInfo> intercepts = [];
            List<DiagnosticInfo> diagnostics = [];

            // Add diagnostic if not partial.
            bool isPartial = @class
                .DeclaringSyntaxReferences
                .Any(syntax =>
                    syntax.GetSyntax() is BaseTypeDeclarationSyntax declaration &&
                    declaration.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword)));

            if (!isPartial)
            {
                diagnostics.Add(new DiagnosticInfo(
                    DiagnosticDescriptors.InterceptorMustBePartial,
                    @class.OriginalDefinition.Locations[0]
                ));
            }

            // Extract target clients.
            Client targetClients = Client.All;

            var interceptsAttribute = @class.GetAttributes().FirstOrDefault(
                x => x.AttributeClass?.ToDisplayString() == Constants.InterceptAttributeMetadataName);

            if (interceptsAttribute is not null &&
                interceptsAttribute.ConstructorArguments.Length > 0 &&
                interceptsAttribute.ConstructorArguments[0].Value is int targetClientValue)
            {
                targetClients = ((Client)targetClientValue) & Client.All;
                if (targetClients == Client.None)
                {
                    diagnostics.Add(new DiagnosticInfo(
                        DiagnosticDescriptors.EmptyTargetClientOnInterceptsAttribute,
                        interceptsAttribute.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                    ));
                }
            }

            // Extract intercept info.
            foreach (ISymbol member in members)
            {
                if (ExtractInterceptInfo(targetClients, member, diagnostics) is { } interceptInfo)
                    intercepts.Add(interceptInfo);
            }

            // Extract namespace.
            string namespaceName = "";
            if (@class.ContainingNamespace.CanBeReferencedByName)
                namespaceName = @class.ContainingNamespace.ToDisplayString();

            return new Result<InterceptorInfo?>(
                new InterceptorInfo(
                    namespaceName,
                    @class.Name,
                    targetClients,
                    intercepts.ToArray()
                ),
                diagnostics.ToArray()
            );
        }

        static InterceptInfo? ExtractInterceptInfo(Client targetClients, ISymbol member, List<DiagnosticInfo> diagnostics)
        {
            if (member is not IMethodSymbol method)
                return null;

            List<Identifier> identifiers = [];
            Client interceptsOn = targetClients;

            bool hasInterceptAttribute = false;
            AttributeData?
                interceptAttribute = null,
                interceptInAttribute = null,
                interceptOutAttribute = null;

            foreach (AttributeData attr in member.GetAttributes())
            {
                if (attr.AttributeClass is null) continue;
                string attributeName = attr.AttributeClass.ToDisplayString();

                if (attributeName == Constants.InterceptAttributeMetadataName)
                {
                    interceptAttribute = attr;
                    hasInterceptAttribute = true;
                    continue;
                }

                Direction direction = GetDirectionForInterceptAttribute(attributeName);
                if (direction == Direction.None) continue;

                hasInterceptAttribute = true;

                if (direction == Direction.In) interceptInAttribute = attr;
                else if (direction == Direction.Out) interceptOutAttribute = attr;

                if (attr.ConstructorArguments.Length != 1)
                    continue;

                var argument = attr.ConstructorArguments[0];
                if (argument.Kind != TypedConstantKind.Array)
                    continue;

                if (argument.Values.Length == 0)
                {
                    diagnostics.Add(new DiagnosticInfo(
                        DiagnosticDescriptors.IdentifiersNotSpecified,
                        attr.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                    ));
                    continue;
                }

                Location[]? attributeParamLocations = null;
                if (attr.ApplicationSyntaxReference?.GetSyntax() is AttributeSyntax attrSyntax)
                    attributeParamLocations = attrSyntax.ArgumentList?.Arguments.Select(x => x.GetLocation()).ToArray();

                for (int i = 0; i < argument.Values.Length; i++)
                {
                    if (argument.Values[i].Value is not string name) continue;

                    Client client = Client.None;

                    int colonIndex = name.IndexOf(':');
                    if (colonIndex >= 0)
                    {
                        string clientIdentifier = name[..colonIndex];
                        if (clientIdentifier.Length == 1)
                            client = clientIdentifier[0].ToClient();

                        if (client == Client.None)
                        {
                            diagnostics.Add(new DiagnosticInfo(
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
                        diagnostics.Add(new DiagnosticInfo(
                            DiagnosticDescriptors.InvalidMessageIdentifier,
                            attributeParamLocations is null
                                ? attr.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                                : attributeParamLocations[i],
                            name
                        ));
                        continue;
                    }

                    identifiers.Add(new Identifier(client, direction, name));
                }
            }

            if (!hasInterceptAttribute) return null;

            string? messageHandlerType = null;

            if (member.IsStatic)
            {
                diagnostics.Add(new DiagnosticInfo(
                    DiagnosticDescriptors.InterceptHandlerMustNotBeStatic,
                    member.Locations[0]
                ));
            }

            if (!AnalysisHelper.IsInterceptHandlerSignature(method))
            {
                if (AnalysisHelper.IsInterceptMessageHandlerSignature(method))
                {
                    messageHandlerType = ((INamedTypeSymbol)method.Parameters[0].Type).TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                }
                else if (AnalysisHelper.IsInterceptMessageHandlerSignature2(method))
                {
                    messageHandlerType = method.Parameters[1].Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                }
                else if (AnalysisHelper.IsMessageHandlerSignature(method) ||
                    AnalysisHelper.IsModifyMessageHandlerSignature(method))
                {
                    messageHandlerType = method.Parameters[0].Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                }
                else
                {
                    diagnostics.Add(new DiagnosticInfo(
                        DiagnosticDescriptors.InvalidInterceptMethodSignature,
                        member.Locations[0]
                    ));
                    return null;
                }
            }

            if (messageHandlerType is not null)
            {
                if (interceptAttribute is { ConstructorArguments.Length: > 0 })
                {
                    diagnostics.Add(new DiagnosticInfo(
                        DiagnosticDescriptors.ClientSpecifierOnMessageHandler,
                        interceptAttribute.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                    ));
                }
                if (interceptInAttribute is not null)
                {
                    diagnostics.Add(new DiagnosticInfo(
                        DiagnosticDescriptors.DirectionalInterceptOnMessageHandler,
                        interceptInAttribute.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                    ));
                }
                if (interceptOutAttribute is not null)
                {
                    diagnostics.Add(new DiagnosticInfo(
                        DiagnosticDescriptors.DirectionalInterceptOnMessageHandler,
                        interceptOutAttribute.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                    ));
                }
            }
            else
            {
                if (interceptAttribute is { ConstructorArguments.Length: 0 })
                {
                    diagnostics.Add(new DiagnosticInfo(
                        DiagnosticDescriptors.NoTargetClientsOnInterceptsAttribute,
                        interceptAttribute.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                    ));
                }
                else if (interceptAttribute is { ConstructorArguments: [ { Value: int clientTypeValue } ] })
                {
                    interceptsOn &= (Client)clientTypeValue;
                    if (interceptsOn == Client.None && targetClients != Client.None)
                    {
                        diagnostics.Add(new DiagnosticInfo(
                            DiagnosticDescriptors.EmptyTargetClientOnInterceptsAttribute,
                            interceptAttribute.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                        ));
                    }
                }
            }

            return new InterceptInfo(
                identifiers.ToArray(),
                interceptsOn,
                member.Name,
                messageHandlerType
            );
        }
    }
}