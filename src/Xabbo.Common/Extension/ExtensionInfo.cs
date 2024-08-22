namespace Xabbo.Extension;

public sealed record ExtensionInfo(
    string? Name = null,
    string? Description = null,
    string? Author = null,
    string? Version = null
);

public class ASdf : IExtensionInfoInit {
    ExtensionInfo IExtensionInfoInit.Info => new(
        Author: "asd"
    );
}