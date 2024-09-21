namespace Xabbo.Common.Generator.Tests;

[Flags]
public enum TestType
{
    Extension = 1 << 0,
    Interceptor = 1 << 1,
    InterceptorContext = 1 << 2,
    ReadImpl = 1 << 3,
    ReplaceImpl = 1 << 4,
    All = unchecked((int)uint.MaxValue)
}

