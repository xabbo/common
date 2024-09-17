using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Xabbo.Common.Generator.Tests;

public static class TestHelper
{
    static bool ShouldIgnore(TestType type, GeneratedSourceResult result)
    {
        if ((type & TestType.Extension) > 0 && result.HintName.EndsWith("Extension.g.cs"))
            return false;
        if ((type & TestType.Interceptor) > 0 && result.HintName.EndsWith("Interceptor.g.cs"))
            return false;
        if ((type & TestType.ReadImpl) > 0 && result.HintName.EndsWith("Read.Impl.g.cs"))
            return false;
        if ((type & TestType.ReplaceImpl) > 0 && result.HintName.EndsWith("Replace.Impl.g.cs"))
            return false;
        return true;
    }

    public static Task Verify(TestType testType, string source)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

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
            syntaxTrees: [syntaxTree],
            references: references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        var error = compilation.GetDiagnostics().Where(x => x.Severity == DiagnosticSeverity.Error).ToList();
        if (error.Count > 0)
        {
            throw new Exception(error[0].GetMessage());
        }

        var generator = new Xabbo.Common.Generator.Generator();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGenerators(compilation);

        var settings = new VerifySettings();
        settings.UseDirectory("Generated");
        settings.UseUniqueDirectory();

        return Verifier.Verify(driver, settings)
            .IgnoreGeneratedResult(result => ShouldIgnore(testType, result));
    }
}