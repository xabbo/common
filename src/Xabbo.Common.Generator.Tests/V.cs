using System.Collections.Immutable;
using System.Reflection;
using Argon;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xabbo.Common.Generator.Tests;

public static class V
{
    static readonly string[] GlobalUsings = [
        "System",
        "System.Threading",
        "System.Threading.Tasks",
        "System.Collections",
        "System.Collections.Generic",
        "Xabbo",
        "Xabbo.Messages",
        "Xabbo.Connection",
        "Xabbo.Interceptor"
    ];

    static readonly JsonSerializerSettings SerializerSettings = new()
    {
        Formatting = Formatting.Indented,
    };

    static readonly Dictionary<TestType, string[]> TestTypeSuffixes = new()
    {
        [TestType.Extension] = ["Extension.g.cs"],
        [TestType.Interceptor] = ["Interceptor.g.cs"],
        [TestType.InterceptorContext] = ["InterceptorContext.g.cs"],
        [TestType.ReadImpl] = ["Read.Impl.g.cs"],
        [TestType.ReplaceImpl] = ["Replace.Impl.g.cs"],
        [TestType.SendHeader] = ["SendHeader.g.cs"],
        [TestType.SendIdentifier] = ["SendIdentifier.g.cs"],
        [TestType.VariadicRead] = ["Read.g.cs", "ReadAt.g.cs"],
        [TestType.VariadicWrite] = ["Write.g.cs", "WriteAt.g.cs"],
        [TestType.VariadicReplace] = ["Replace.g.cs", "ReplaceAt.g.cs"],
        [TestType.VariadicModify] = ["Modify.g.cs", "ModifyAt.g.cs"],
    };

    static bool ShouldIgnore(TestType typesToTest, GeneratedSourceResult result)
    {
        // Ignore static files
        if (result.HintName.EndsWith("Base.g.cs"))
            return true;

        foreach (var (testType, suffixes) in TestTypeSuffixes)
        {
            if (typesToTest.HasFlag(testType) && suffixes.Any(suffix => result.HintName.EndsWith(suffix)))
                return false;
        }

        return true;
    }

    public static void Diagnostic(string source, DiagnosticSeverity? severity = null, params object?[] args) => Source(
        source,
        TestType.Diagnostics,
        isScript: true,
        severity: severity,
        args: args
    );

    public static void DiagnosticClass(string source, DiagnosticSeverity? expectedSeverity = null, string? paramText = null, params object?[] args) => Source(
        source,
        TestType.Diagnostics,
        isScript: false,
        severity: expectedSeverity,
        paramText: paramText,
        args: args
    );

    public static void Script(
        string source,
        TestType testType = TestType.All,
        DiagnosticSeverity? severity = null,
        string? paramText = null,
        params object?[] args)
    {
        Source(
            source,
            testType,
            isScript: true,
            severity: severity,
            paramText: paramText,
            args: args
        );
    }

    public static void Source(
        string source,
        TestType testType = TestType.All,
        bool isScript = false,
        DiagnosticSeverity? severity = null,
        string? paramText = null,
        params object?[] args)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        SyntaxTree globalUsings = CSharpSyntaxTree.ParseText(
            string.Join('\n', GlobalUsings.Select(x => $"global using global::{x};")));

        List<MetadataReference> references = [
            // mscorlib
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            // System.Runtime
            MetadataReference.CreateFromFile(typeof(System.Attribute).Assembly.Location),
            // Xabbo.Common
            MetadataReference.CreateFromFile(typeof(Hotel).Assembly.Location),
        ];

        if (Assembly.GetEntryAssembly() is { } assembly)
        {
            foreach (var reference in assembly.GetReferencedAssemblies())
            {
                references.Add(MetadataReference.CreateFromFile(Assembly.Load(reference).Location));
            }
        }

        CSharpCompilation preGenCompilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: [globalUsings, syntaxTree],
            references: references,
            new CSharpCompilationOptions(
                isScript ? OutputKind.ConsoleApplication : OutputKind.DynamicallyLinkedLibrary,
                scriptClassName: isScript ? "TestHelper" : null
            )
        );

        var generator = new Xabbo.Common.Generator.Generator();

        var driver = CSharpGeneratorDriver.Create(generator)
            .RunGeneratorsAndUpdateCompilation(
                preGenCompilation,
                out Compilation postGenCompilation,
                out ImmutableArray<Diagnostic> diagnostics
            );

        var postGenCompilationWithAnalyzers = postGenCompilation.WithAnalyzers([
            new PacketTypeAnalyzer(),
            new InterceptorAnalyzer()
        ]);

        diagnostics = postGenCompilationWithAnalyzers.GetAllDiagnosticsAsync().GetAwaiter().GetResult()
            .Concat(diagnostics)
            .Where(x => x.Id.StartsWith("XABBO") || x.Severity >= DiagnosticSeverity.Error)
            .ToImmutableArray();

        List<Target> targets = [];

        var verifyTask = Verifier
            .Verify(driver)
            .UseDirectory("Snapshots")
            .UseUniqueDirectory()
            .IgnoreGeneratedResult(result => ShouldIgnore(testType, result));

        if (diagnostics.Any())
        {
            JsonSerializerSettings serializerSettings = new();
            verifyTask.AddExtraSettings(x => serializerSettings = x);

            var serializer = JsonSerializer.Create(serializerSettings);

            var writer = new StringWriter();
            serializer.Serialize(writer, diagnostics.Select(x => DiagnosticInfo.Create(x)));

            verifyTask.AppendContentAsFile(
                writer.ToString(),
                name: "Diagnostics"
            );
        }

        if (paramText is not null)
            verifyTask = verifyTask.UseTextForParameters(paramText);
        else if (args is { Length: > 0 })
            verifyTask = verifyTask.UseParameters(args);

        verifyTask?.GetAwaiter().GetResult();

        if (severity is null)
        {
            if (diagnostics.Any(x => x.Severity >= DiagnosticSeverity.Error))
                Assert.Fail($"Expected no errors, got {diagnostics.First()}");
        }
        else
        {
            var minimumSeverity = (DiagnosticSeverity)(-1);
            if (diagnostics.Length > 0)
                minimumSeverity = diagnostics.Min(x => x.Severity);
            if (minimumSeverity != severity)
                Assert.Fail($"Expected minimum {severity} severity, got {minimumSeverity}");
        }
    }
}
