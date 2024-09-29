using System.Collections.Immutable;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Xabbo.Common.Generator.Tests;

public static class TestHelper
{
    static readonly string[] GlobalUsings = [
        "System",
        "System.Threading",
        "System.Threading.Tasks",
        "Xabbo",
        "Xabbo.Messages",
        "Xabbo.Interceptor"
    ];

    static readonly Dictionary<TestType, string> TestTypeSuffixes = new()
    {
        [TestType.Extension] = "Extension.g.cs",
        [TestType.Interceptor] = "Interceptor.g.cs",
        [TestType.InterceptorContext] = "InterceptorContext.g.cs",
        [TestType.ReadImpl] = "Read.Impl.g.cs",
        [TestType.ReplaceImpl] = "Replace.Impl.g.cs",
        [TestType.SendHeader] = "SendHeader.g.cs",
        [TestType.SendIdentifier] = "SendIdentifier.g.cs",
    };

    static bool ShouldIgnore(TestType typesToTest, GeneratedSourceResult result)
    {
        // Ignore static files
        if (result.HintName.EndsWith("Base.g.cs"))
            return true;

        foreach (var (testType, suffix) in TestTypeSuffixes)
        {
            if (result.HintName.EndsWith(suffix) && !typesToTest.HasFlag(testType))
                return true;
        }
        return false;
    }

    public static Task Verify(
        string source,
        TestType testType = TestType.All,
        bool isScript = false,
        bool checkCompilationErrors = true)
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

        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: [globalUsings, syntaxTree],
            references: references,
            new CSharpCompilationOptions(
                isScript ? OutputKind.ConsoleApplication : OutputKind.DynamicallyLinkedLibrary,
                scriptClassName: "TestHelper"
            )
        );

        var generator = new Xabbo.Common.Generator.Generator();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation,
            out Compilation generatedCompilation,
            out ImmutableArray<Diagnostic> diagnostics);

        var generatedDiagnostics = generatedCompilation.GetDiagnostics();
        var errors = generatedDiagnostics
            .Where(x => x.Severity == DiagnosticSeverity.Error)
            .ToList();

        if (checkCompilationErrors && errors.Count > 0)
            throw new Exception(string.Join('\n', errors.Select(e => e.ToString())));

        var settings = new VerifySettings();
        settings.UseDirectory("Generated");
        settings.UseUniqueDirectory();

        return Verifier.Verify(driver, settings)
            .IgnoreGeneratedResult(result => ShouldIgnore(testType, result));
    }
}