using System;

using Xabbo.Extension;

namespace Xabbo.Common.Tests;

public partial class SampleInterceptor : IExtensionInfoInit
{
    ExtensionInfo IExtensionInfoInit.Info => new(
        Name: "Extension title",
        Description: "Extension description"
    );
}
