using System.Runtime.InteropServices.ComTypes;

namespace Xabbo.Common.Generator.Model;

[Flags]
internal enum InvocationKind
{
    Read = 0x01,
    Write = 0x02,
    Replace = 0x04,
    Modify = 0x08,
    Send = 0x10,

    At = 0x8000,
    Header = 0x4000,
    Identifier = 0x2000,
    Message = 0x1000,

    ReadAt = Read | At,
    WriteAt = Write | At,
    ReplaceAt = Replace | At,
    ModifyAt = Modify | At,

    SendHeader = Send | Header,
    SendIdentifier = Send | Identifier,
    SendMessage = Send | Message,

    HasArguments = Write | Replace | Modify | Send,
    RequiresParser = Read | Replace | Modify,
    RequiresComposer = Write | Replace | Modify | Send,
    RequiresParserComposer = RequiresParser & RequiresComposer,

    SupportsArray = Read | Write | Send,
    SupportsEnumerable = Send | Write,
    SupportsFunc = Modify,

    /// <summary>
    /// Whether the invocation has a fixed first argument:
    /// <list type="bullet">
    /// <item>ReadAt(position, ...)</item>
    /// <item>WriteAt(position, ...)</item>
    /// <item>Send(header, ...)</item>
    /// <item>Send(identifier, ...)</item>
    /// </list>
    /// </summary>
    HasFixedFirstArg = At | Send
}

internal static class EnumExtensions
{
    public static string GetMethodName(this InvocationKind kind) => kind switch
    {
        InvocationKind.Read => "Read",
        InvocationKind.ReadAt => "ReadAt",
        InvocationKind.Write => "Write",
        InvocationKind.WriteAt => "WriteAt",
        InvocationKind.Replace => "Replace",
        InvocationKind.ReplaceAt => "ReplaceAt",
        InvocationKind.Modify => "Modify",
        InvocationKind.ModifyAt => "ModifyAt",
        InvocationKind.Send or
        InvocationKind.SendHeader or
        InvocationKind.SendIdentifier => "Send",
        _ => "?"
    };
}