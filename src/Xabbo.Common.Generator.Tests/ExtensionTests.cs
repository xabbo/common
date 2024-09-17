namespace Xabbo.Common.Generator.Tests;

public class ExtensionTests
{
    [Fact]
    public Task TestExtension() => TestHelper.Verify(TestType.Extension, @"
        using Xabbo;

        namespace ExtensionTests;

        [Extension(
            Name = ""Name"",
            Description = ""Description"",
            Author = ""Author"",
            Version = ""1.0""
        )]
        partial class TestExtension { }
    ");

    [Fact]
    public Task TestPartialExtension() => TestHelper.Verify(TestType.Extension, @"
        using Xabbo;

        namespace ExtensionTests;

        [Extension(Name = ""Name"")]
        partial class PartialExtension { }
    ");

    [Fact]
    public Task TestEmptyExtension() => TestHelper.Verify(TestType.Extension, @"
        using Xabbo;

        namespace ExtensionTests;

        [Extension]
        partial class EmptyExtension { }
    ");

}