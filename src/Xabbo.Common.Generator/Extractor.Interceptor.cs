using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

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

        internal static InterceptorInfo? ExtractInfo(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (context.SemanticModel.GetDeclaredSymbol(context.TargetNode) is not INamedTypeSymbol @class)
                return null;

            // Extract target clients.
            Client targetClients = Client.All;

            var interceptAttribute = @class.GetAttributes().FirstOrDefault(
                x => AnalysisHelper.IsInterceptAttribute(x.AttributeClass));

            if (interceptAttribute?.ConstructorArguments is [ { Value: int targetClientValue } ])
                targetClients &= (Client)targetClientValue;

            // Extract intercept info.
            List<InterceptInfo> intercepts = [];
            foreach (ISymbol member in @class.GetMembers())
            {
                if (ExtractInterceptInfo(targetClients, member) is { } interceptInfo)
                    intercepts.Add(interceptInfo);
            }

            // Extract namespace.
            string namespaceName = "";
            if (@class.ContainingNamespace.CanBeReferencedByName)
                namespaceName = @class.ContainingNamespace.ToDisplayString();

            return new InterceptorInfo(
                namespaceName,
                @class.Name,
                targetClients,
                intercepts.ToArray()
            );
        }

        static InterceptInfo? ExtractInterceptInfo(Client targetClients, ISymbol member)
        {
            if (member is not IMethodSymbol method)
                return null;

            List<Identifier> identifiers = [];

            bool hasInterceptAttribute = false;

            // Collect directional intercept identifier names
            foreach (AttributeData attr in member.GetAttributes())
            {
                if (attr.AttributeClass is null) continue;
                string attributeName = attr.AttributeClass.ToDisplayString();

                if (attributeName == Constants.InterceptAttributeMetadataName)
                {
                    hasInterceptAttribute = true;
                    if (attr.ConstructorArguments is [ { Value: int targetClientValue } ])
                        targetClients &= (Client)targetClientValue;
                    continue;
                }

                Direction direction = GetDirectionForInterceptAttribute(attributeName);
                if (direction == Direction.None) continue;

                hasInterceptAttribute = true;

                if (attr.ConstructorArguments is not [ { Values: var identifierArgs } ])
                    continue;

                foreach (var identifierArg in identifierArgs)
                {
                    if (identifierArg.Value is not string name) continue;

                    Client client = Client.None;

                    int colonIndex = name.IndexOf(':');
                    if (colonIndex >= 0)
                    {
                        string clientIdentifier = name[..colonIndex];
                        if (clientIdentifier.Length == 1)
                            client = clientIdentifier[0].ToClient();
                        name = name[(colonIndex + 1)..];
                    }

                    identifiers.Add(new Identifier(client, direction, name));
                }
            }

            if (!hasInterceptAttribute) return null;

            string? messageHandlerType = null;

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
                    return null;
                }
            }

            return new InterceptInfo(
                identifiers.ToArray(),
                targetClients,
                member.Name,
                messageHandlerType
            );
        }
    }
}