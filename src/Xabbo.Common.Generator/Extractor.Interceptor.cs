using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Xabbo.Common.Generator.Diagnostics;
using Xabbo.Common.Generator.Model;

namespace Xabbo.Common.Generator;

internal static partial class Extractor
{
    internal static class Interceptor
    {
        static Direction GetDirectionForInterceptAttribute(string name) => name switch
        {
            "Xabbo.InterceptInAttribute" => Direction.In,
            "Xabbo.InterceptOutAttribute" => Direction.Out,
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

            foreach (ISymbol member in members)
            {
                InterceptInfo? interceptInfo = ExtractInterceptInfo(member, diagnostics);
                if (interceptInfo is { } value)
                    intercepts.Add(value);
            }

            string namespaceName = "";
            if (@class.ContainingNamespace.CanBeReferencedByName)
                namespaceName = @class.ContainingNamespace.ToDisplayString();

            return new Result<InterceptorInfo?>(
                new InterceptorInfo(
                    namespaceName,
                    @class.Name,
                    intercepts.ToArray()
                ),
                diagnostics.ToArray()
            );
        }

        static InterceptInfo? ExtractInterceptInfo(ISymbol member, List<DiagnosticInfo> diagnostics)
        {
            List<Identifier> identifiers = [];
            Client interceptsOn = Client.All;

            bool hasInterceptAttribute = false;

            foreach (AttributeData attr in member.GetAttributes())
            {
                if (attr.AttributeClass is null) continue;
                string attributeName = attr.AttributeClass.ToDisplayString();

                if (attributeName == Constants.InterceptsOnAttributeMetadataName)
                {
                    if (attr.ConstructorArguments[0].Value is int clientTypeValue)
                    {
                        interceptsOn = (Client)clientTypeValue;
                    }
                    continue;
                }

                Direction direction = GetDirectionForInterceptAttribute(attributeName);
                if (direction == Direction.None) continue;

                hasInterceptAttribute = true;

                if (attr.ConstructorArguments.Length != 1)
                    throw new Exception("arguments len must be 1");

                var argument = attr.ConstructorArguments[0];

                if (argument.Kind != TypedConstantKind.Array)
                    throw new Exception("argument kind is " + argument.Kind);

                if (argument.Values.Length == 0)
                {
                    diagnostics.Add(new DiagnosticInfo(
                        DiagnosticDescriptors.IdentifiersNotSpecified,
                        attr.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                    ));
                }
                else
                {
                    foreach (TypedConstant nameConstant in argument.Values)
                    {
                        if (nameConstant.Value is not string name) continue;

                        Client client = Client.None;

                        int colonIndex = name.IndexOf(':');
                        if (colonIndex >= 0) {
                            string clientIdentifier = name.Substring(0, colonIndex);
                            if (clientIdentifier.Length == 1)
                                client = clientIdentifier[0].ToClient();

                            if (client == Client.None)
                            {
                                diagnostics.Add(new DiagnosticInfo(
                                    DiagnosticDescriptors.InvalidSingleClientSpecifier,
                                    attr.ApplicationSyntaxReference?.GetSyntax().GetLocation(),
                                    name.Substring(0, colonIndex + 1)
                                ));
                                continue;
                            }

                            name = name.Substring(colonIndex + 1);
                        }

                        if (!Identifier.IsValid(name))
                        {
                            diagnostics.Add(new DiagnosticInfo(
                                DiagnosticDescriptors.InvalidMessageIdentifier,
                                attr.ApplicationSyntaxReference?.GetSyntax().GetLocation(),
                                name
                            ));
                            continue;
                        }

                        identifiers.Add(new Identifier(client, direction, name));
                    }
                }
            }

            if (!hasInterceptAttribute) return null;

            if (member.IsStatic)
            {
                diagnostics.Add(new DiagnosticInfo(
                    DiagnosticDescriptors.InterceptHandlerMustNotBeStatic,
                    member.Locations[0]
                ));
            }

            return new InterceptInfo(
                identifiers.ToArray(),
                interceptsOn,
                member.Name
            );
        }
    }
}