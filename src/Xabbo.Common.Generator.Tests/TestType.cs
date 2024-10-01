namespace Xabbo.Common.Generator.Tests;

[Flags]
public enum TestType
{
    CompilationOnly = 0,
    Extension = 1 << 0,
    Interceptor = 1 << 1,
    InterceptorContext = 1 << 2,
    ReadImpl = 1 << 3,
    ReplaceImpl = 1 << 4,
    SendHeader = 1 << 5,
    SendIdentifier = 1 << 6,
    Read = 1 << 7,
    Write = 1 << 8,
    All = unchecked((int)uint.MaxValue)
}

