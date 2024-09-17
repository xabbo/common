//HintName: ExtensionTests.TestExtension.Extension.g.cs
using System;

using Xabbo.Extension;

namespace ExtensionTests;

public partial class TestExtension : IExtensionInfoInit
{
    ExtensionInfo IExtensionInfoInit.Info => new(
        Name: "Name",
        Description: "Description",
        Author: "Author",
        Version: "1.0"
    );
}
