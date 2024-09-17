//HintName: ExtensionTests.PartialExtension.Extension.g.cs
using System;

using Xabbo.Extension;

namespace ExtensionTests;

public partial class PartialExtension : IExtensionInfoInit
{
    ExtensionInfo IExtensionInfoInit.Info => new(
        Name: "Name"
    );
}
