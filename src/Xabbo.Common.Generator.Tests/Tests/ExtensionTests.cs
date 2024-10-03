namespace Xabbo.Common.Generator.Tests;

public class ExtensionTests
{
    [Fact]
    public void Extension() => V.Source(
        @"
            namespace ExtensionTests;

            [Extension(
                Name = ""Name"",
                Description = ""Description"",
                Author = ""Author"",
                Version = ""1.0""
            )]
            partial class TestExtension { }
        ",
        testType: TestType.Extension
    );

    [Fact]
    public void PartialExtension() => V.Source(
        @"
            namespace ExtensionTests;

            [Extension(Name = ""Name"")]
            partial class PartialExtension { }
        ",
        testType: TestType.Extension
    );

    [Fact]
    public void EmptyExtension() => V.Source(
        @"
            namespace ExtensionTests;

            [Extension]
            partial class EmptyExtension { }
        ",
        testType: TestType.Extension
    );

    [Fact]
    public void GlobalNamespaceExtension() => V.Source(
        @"
            [Extension(Name = ""Name"")] partial class GlobalExtension { }
        ",
        testType: TestType.Extension
    );

    [Fact]
    public void StringInjection() => V.Source(
        @"
            namespace ExtensionTests;

            [Extension(
                Name = ""Te\""st""
            )]
            partial class TestExtension { }
        ",
        testType: TestType.Extension
    );
}
