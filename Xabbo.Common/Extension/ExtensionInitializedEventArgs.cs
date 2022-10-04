using System;

namespace Xabbo.Extension;

public class ExtensionInitializedEventArgs : EventArgs
{
    public bool? IsGameConnected { get; }

    public ExtensionInitializedEventArgs(bool? isGameConnected = null)
    {
        IsGameConnected = isGameConnected;
    }
}
