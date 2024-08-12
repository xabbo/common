namespace Xabbo.Common.Generators;

public static class SourceGenerationHelper
{
    public const string Attribute = @"
namespace Xabbo
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class InterceptorAttribute : System.Attribute { }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class InterceptAttribute : System.Attribute { }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class InterceptInAttribute : InterceptAttribute { }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class InterceptOutAttribute : InterceptAttribute { }
}";
}
