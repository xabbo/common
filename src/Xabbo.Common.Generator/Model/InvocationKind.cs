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
    RequiresParserComposer = RequiresParser & RequiresComposer
}
