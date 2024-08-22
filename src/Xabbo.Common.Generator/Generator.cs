using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Xabbo.Common.Generator.Model;

namespace Xabbo.Common.Generator;

[Generator(LanguageNames.CSharp)]
public class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Select classes marked with [Extension]
        IncrementalValuesProvider<GeneratorAttributeSyntaxContext> extensionContexts = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                Constants.ExtensionAttributeMetadataName,
                predicate: static(node, _) => node is ClassDeclarationSyntax,
                transform: static(ctx, _) => ctx
            );

        // Extract ExtensionInfo
        IncrementalValuesProvider<Result<ExtensionInfo?>> extensionResults = extensionContexts.Select(Extractor.Extension.ExtractInfo);

        // Report extension diagnostics
        context.RegisterSourceOutput(
            extensionResults.Select(static (extensionResult, _) => extensionResult.Errors),
            Executor.ReportDiagnostics
        );

        // Generate IExtensionInfoInit implementations for [Extension] classes
        context.RegisterSourceOutput(
            extensionResults
                .Where(static (m) => m.Value is not null)
                .Select(static (m, _) => m.Value!),
            Executor.Extension.Execute
        );

        // Select classes marked with [Intercepts]
        IncrementalValuesProvider<GeneratorAttributeSyntaxContext> interceptorContexts = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                Constants.InterceptsAttributeMetadataName,
                predicate: static (node, _) => node is ClassDeclarationSyntax,
                transform: static (ctx, _) => ctx
            );

        // Combine [Extension] and [Intercepts]
        IncrementalValuesProvider<GeneratorAttributeSyntaxContext> interceptorAndExtensionContexts = interceptorContexts.Collect()
            .Combine(extensionContexts.Collect())
            .SelectMany((x, _) => {
                HashSet<string> seen = [];
                foreach (var item in x.Left)
                    seen.Add(item.TargetSymbol.ToDisplayString());
                return x.Left.AddRange(x.Right.Where(x => seen.Add(x.TargetSymbol.ToDisplayString())));
            });

        // Extract InterceptorInfo
        IncrementalValuesProvider<Result<InterceptorInfo?>> interceptorResults = interceptorAndExtensionContexts
            .Select(Extractor.Interceptor.ExtractInfo);

        // Report interceptor diagnostics
        context.RegisterSourceOutput(
            interceptorResults
                .Select(static (interceptorResult, _) => interceptorResult.Errors),
            Executor.ReportDiagnostics
        );

        // Generate intercept handlers
        context.RegisterSourceOutput(
            interceptorResults
                .Where(static (m) => m.Value is not null)
                .Select(static (m, _) => m.Value!),
            Executor.Interceptor.Execute
        );
    }
}