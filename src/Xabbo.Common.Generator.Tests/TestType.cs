namespace Xabbo.Common.Generator.Tests;

[Flags]
public enum TestType
{
    Diagnostics = 0,
    Extension = 1 << 0,
    Interceptor = 1 << 1,
    InterceptorContext = 1 << 2,
    ReadImpl = 1 << 3,
    ReplaceImpl = 1 << 4,
    ModifyImpl = ReadImpl | ReplaceImpl,
    SendHeader = 1 << 5,
    SendIdentifier = 1 << 6,
    VariadicRead = 1 << 7,
    VariadicWrite = 1 << 8,
    VariadicReplace = 1 << 9,
    VariadicModify = 1 << 10,
    VariadicSend = SendHeader | SendIdentifier,

    All = -1
}

