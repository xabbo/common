//HintName: ExtensionTests.EmptyExtension.Extension.g.cs
using System;

using Xabbo.Extension;

namespace ExtensionTests;

public partial class EmptyExtension : IExtensionInfoInit
{
    ExtensionInfo IExtensionInfoInit.Info => new(
    );
}
