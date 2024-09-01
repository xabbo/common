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
                predicate: static (node, _) => node is ClassDeclarationSyntax,
                transform: static (ctx, _) => ctx
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
            .SelectMany((x, _) =>
            {
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

        // Variadic source generation
        context.RegisterPostInitializationOutput(Executor.Variadic.RegisterPostInitializationOutput);

        // Collect all invocations of Read, Write, Replace, Modify and Send
        var invocationResults = context.SyntaxProvider.CreateSyntaxProvider(
            static (node, _) => Extractor.Variadic.IsCandidateForGeneration(node),
            Extractor.Variadic.ExtractVariadicInvocation
        );

        // Report diagnostics
        context.RegisterSourceOutput(
            invocationResults.SelectMany((result, _) => result.Errors),
            Executor.ReportDiagnostic
        );

        // Extract invocations
        IncrementalValuesProvider<VariadicInvocation> invocations = invocationResults
            .Where(static (x) => x.Value is not null)
            .Select(static (x, _) => x.Value!);

        // Output source for each invocation kind / arity
        foreach (var invocationKind in Executor.Variadic.InvocationGenerationKinds)
        {
            int minimumArity = (invocationKind & InvocationKind.Send) > 0 ? 1 : 2;

            IncrementalValueProvider<EquatableArray<int>> arities = invocations
                .Where(invocation => invocation.Kind == invocationKind)
                .Collect()
                .Select((invocations, _) => invocations
                    .Select(invocation => invocation.Types.Length)
                    .Where(x => x >= minimumArity)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToEquatableArray()
                );

            context.RegisterSourceOutput(arities, (context, arities) => Executor.Variadic.GenerateInvocationArities(context, invocationKind, arities));
        }

        // Get distinct types required for Read<T>
        IncrementalValueProvider<EquatableArray<VariadicType>> distinctReadTypes = invocations
            .Where(x => (x.Kind & (InvocationKind.RequiresParser)) > 0)
            .SelectMany((x, _) => x.Types)
            .Collect()
            .Select((types, _) =>
            {
                HashSet<VariadicType> distinctTypes = [];
                foreach (var type in types.Where(type => type.IsParser || type.IsArray))
                {
                    distinctTypes.Add(type);
                    if (type.IsParser && type.IsArray)
                        distinctTypes.Add(type with { IsArray = false });
                }
                return distinctTypes.ToEquatableArray();
            });

        // Generate Read<T> implementation
        context.RegisterSourceOutput(distinctReadTypes, Executor.Variadic.GenerateReadImplementation);

        // Get distinct IParserComposer<T> types required for Replace<T>
        IncrementalValueProvider<EquatableArray<VariadicType>> distinctParserComposerTypes = invocations
            .Where(x => (x.Kind & InvocationKind.RequiresParserComposer) > 0)
            .SelectMany((x, _) => x.Types)
            .Collect()
            .Select((types, _) =>
            {
                HashSet<VariadicType> distinctTypes = [];
                foreach (var type in types.Where(t => t.IsParser && t.IsComposer))
                    distinctTypes.Add(type);
                return distinctTypes.ToEquatableArray();
            });

        // Generate Replace<T> implementation
        context.RegisterSourceOutput(distinctParserComposerTypes, Executor.Variadic.GenerateReplaceImplementation);
    }
}