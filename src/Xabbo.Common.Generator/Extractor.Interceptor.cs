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
                x => x.AttributeClass?.ToDisplayString() == Constants.InterceptsAttributeMetadataName);

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

            foreach (AttributeData attr in member.GetAttributes())
            {
                if (attr.AttributeClass is null) continue;
                string attributeName = attr.AttributeClass.ToDisplayString();

                if (attributeName == Constants.InterceptsAttributeMetadataName)
                {
                    if (attr.ConstructorArguments.Length == 0)
                    {
                        diagnostics.Add(new DiagnosticInfo(
                            DiagnosticDescriptors.NoTargetClientsOnInterceptsAttribute,
                            attr.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                        ));
                    }
                    else
                    {
                        if (attr.ConstructorArguments[0].Value is int clientTypeValue)
                        {
                            interceptsOn &= (Client)clientTypeValue;
                            if (interceptsOn == Client.None && targetClients != Client.None)
                            {
                                diagnostics.Add(new DiagnosticInfo(
                                    DiagnosticDescriptors.EmptyTargetClientOnInterceptsAttribute,
                                    attr.ApplicationSyntaxReference?.GetSyntax().GetLocation()
                                ));
                            }
                        }
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

            if (method is not {
                ReturnsVoid: true,
                Parameters: [{
                    RefKind: RefKind.None,
                    Type: {
                        ContainingNamespace: {
                            ContainingNamespace.IsGlobalNamespace: true,
                            Name: "Xabbo"
                        },
                        Name: "Intercept"
                    }
                }]
            })
            {
                diagnostics.Add(new DiagnosticInfo(
                    DiagnosticDescriptors.InvalidInterceptMethodSignature,
                    method.Locations.FirstOrDefault()
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