namespace Xabbo.Common.Generator.Tests;

public class ExtensionTests
{
    [Fact]
    public Task TestExtension() => TestHelper.Verify(@"
        namespace ExtensionTests;

        [Extension(
            Name = ""Name"",
            Description = ""Description"",
            Author = ""Author"",
            Version = ""1.0""
        )]
        partial class TestExtension { }",
        testType: TestType.Extension);

    [Fact]
    public Task TestPartialExtension() => TestHelper.Verify(@"
        namespace ExtensionTests;

        [Extension(Name = ""Name"")]
        partial class PartialExtension { }",
        testType: TestType.Extension);

    [Fact]
    public Task TestEmptyExtension() => TestHelper.Verify(@"
        namespace ExtensionTests;

        [Extension]
        partial class EmptyExtension { }",
        testType: TestType.Extension);

    [Fact]
    public Task TestGlobalNamespaceExtension() => TestHelper.Verify(@"
        [Extension(Name = ""Name"")] partial class GlobalExtension { }",
        testType: TestType.Extension);
}