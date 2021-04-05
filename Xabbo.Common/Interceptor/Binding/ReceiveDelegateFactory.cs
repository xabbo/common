using System;
using System.Collections.Concurrent;
using System.Reflection;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Binding
{
    internal static class ReceiveDelegateFactory
    {
        private static readonly ConcurrentDictionary<MethodInfo, Delegate> _cache = new();

        public static Action<object, object?, IReadOnlyPacket> GetOpenDelegate(MethodInfo method)
        {
            if (!_cache.TryGetValue(method, out Delegate? @delegate))
            {
                MethodInfo? generatorMethod = typeof(ReceiveDelegateFactory).GetMethod(
                    nameof(GenerateWeaklyTypedOpenDelegate),
                    BindingFlags.NonPublic | BindingFlags.Static
                );

                if (generatorMethod is null)
                {
                    throw new Exception("Failed to get delegate generator method");
                }

                if (method.DeclaringType is null)
                {
                    throw new Exception("The declaring type of the generator method is null");
                }
                
                MethodInfo generator = generatorMethod.MakeGenericMethod(method.DeclaringType);

                @delegate = (Delegate?)generatorMethod.Invoke(null, new object[] { method });

                if (@delegate is null)
                {
                    throw new Exception("Failed to generate delegate");
                }

                _cache.TryAdd(method, @delegate);
            }

            return (Action<object, object?, IReadOnlyPacket>)@delegate;
        }

        private static Action<object, object?, IReadOnlyPacket> GenerateWeaklyTypedOpenDelegate<TTarget>(MethodInfo method)
        {
            var param = method.GetParameters();
            if (param.Length == 0)
            {
                var call = (Action<TTarget>)method.CreateDelegate(typeof(Action<TTarget>));
                return new Action<object, object?, IReadOnlyPacket>((target, sender, packet) => call((TTarget)target));
            }
            else if (param.Length == 1)
            {
                if (param[0].ParameterType == typeof(object))
                {
                    var call = (Action<TTarget, object?>)method.CreateDelegate(typeof(Action<TTarget, object?>));
                    return new Action<object, object?, IReadOnlyPacket>((target, sender, packet) => call((TTarget)target, sender));
                }
                else if (param[0].ParameterType.IsAssignableFrom(typeof(IReadOnlyPacket)))
                {
                    var call = (Action<TTarget, IReadOnlyPacket>)method.CreateDelegate(typeof(Action<TTarget, IReadOnlyPacket>));
                    return new Action<object, object?, IReadOnlyPacket>((target, sender, packet) => call((TTarget)target, packet));
                }
            }
            else if (param.Length == 2)
            {
                if (param[0].ParameterType == typeof(object) &&
                    param[1].ParameterType.IsAssignableFrom(typeof(IReadOnlyPacket)))
                {
                    var call = (Action<TTarget, object?, IReadOnlyPacket>)method.CreateDelegate(typeof(Action<TTarget, object, IReadOnlyPacket>));
                    return new Action<object, object?, IReadOnlyPacket>((target, sender, packet) => call((TTarget)target, sender, packet));
                }
            }

            throw new Exception($"Unable to generate delegate, method {method.DeclaringType?.FullName ?? "?"}.{method.Name} has an invalid signature");
        }
    }
}
