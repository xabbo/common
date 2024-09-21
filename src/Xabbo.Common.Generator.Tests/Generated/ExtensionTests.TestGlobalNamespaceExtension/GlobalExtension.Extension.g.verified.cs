//HintName: GlobalExtension.Extension.g.cs
using System;

using Xabbo.Extension;

public partial class GlobalExtension : IExtensionInfoInit
{
    ExtensionInfo IExtensionInfoInit.Info => new(
        Name: "Name"
    );
}
