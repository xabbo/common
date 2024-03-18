using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Xabbo.Messages.Dispatcher;

internal static class InterceptDelegateFactory
{
    private static readonly ConcurrentDictionary<MethodInfo, Delegate> _cache = new();

    public static Action<object, InterceptArgs> GetOpenDelegate(MethodInfo methodInfo)
    {
        if (!_cache.TryGetValue(methodInfo, out Delegate? @delegate))
        {
            MethodInfo? delegateGenerator = typeof(InterceptDelegateFactory).GetMethod(
                nameof(GenerateWeaklyTypedOpenDelegate),
                BindingFlags.NonPublic | BindingFlags.Static
            ) ?? throw new Exception("Failed to get delegate generator method");

            if (methodInfo.DeclaringType is null)
                throw new InvalidOperationException("The declaring type of the generator method is null");

            var generator = delegateGenerator.MakeGenericMethod(methodInfo.DeclaringType);
            @delegate = (Delegate?)generator.Invoke(null, new object[] { methodInfo });

            if (@delegate is null)
                throw new Exception("Failed to generate delegate");
        }

        return (Action<object, InterceptArgs>)@delegate;
    }

    private static Action<object, InterceptArgs> GenerateWeaklyTypedOpenDelegate<TTarget>(MethodInfo methodInfo)
    {
        var param = methodInfo.GetParameters();
        if (param.Length != 1 ||
            !param[0].ParameterType.Equals(typeof(InterceptArgs)))
        {
            throw new Exception(
                $"Unable to generate delegate, method "
                + $"{methodInfo.DeclaringType?.FullName ?? "?"}.{methodInfo.Name} has an invalid signature"
            );
        }

        var call = (Action<TTarget, InterceptArgs>)methodInfo.CreateDelegate(typeof(Action<TTarget, InterceptArgs>));
        return new Action<object, InterceptArgs>((target, args) => call((TTarget)target, args));
    }
}
