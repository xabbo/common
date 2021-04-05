using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Xabbo.Messages;
using Xabbo.Utility;

namespace Xabbo.Interceptor.Binding
{
    public class InterceptorBinder : IInterceptorBinder
    {
        private static ReceiveCallback CreateCallback(short header, object target, MethodInfo method)
        {
            var callback = ReceiveDelegateFactory.GetOpenDelegate(method);
            return new OpenReceiveCallback(header, target, callback);
        }

        private static InterceptCallback CreateInterceptCallback(Destination destination, short header,
            object target, MethodInfo method)
        {
            var callback = InterceptDelegateFactory.GetOpenDelegate(method);
            return new OpenInterceptCallback(destination, header, target, method, callback);
        }

        private static IReadOnlyList<ReceiveCallback> ReceiverCallbackListFactory(short _)
            => new List<ReceiveCallback>();

        private static IReadOnlyList<InterceptCallback> InterceptCallbackListFactory(short _)
            => new List<InterceptCallback>();

        private bool _disposed;

        private readonly ConcurrentDictionary<object, InterceptorBinding> _bindings;
        private readonly ConcurrentDictionary<short, IReadOnlyList<ReceiveCallback>> _receiveCallbacks;
        private readonly ConcurrentDictionary<short, IReadOnlyList<InterceptCallback>> _incomingInterceptCallbacks;
        private readonly ConcurrentDictionary<short, IReadOnlyList<InterceptCallback>> _outgoingInterceptCallbacks;

        private ConcurrentDictionary<short, IReadOnlyList<InterceptCallback>>
            GetInterceptCallbackDictionary(Destination destination) =>
            destination == Destination.Server ? _outgoingInterceptCallbacks : _incomingInterceptCallbacks;

        public IInterceptor Interceptor { get; }
        public IMessageManager Messages => Interceptor.Messages;

        public InterceptorBinder(IInterceptor interceptor)
        {
            Interceptor = interceptor;

            _bindings = new ConcurrentDictionary<object, InterceptorBinding>();
            _receiveCallbacks = new ConcurrentDictionary<short, IReadOnlyList<ReceiveCallback>>();
            _incomingInterceptCallbacks = new ConcurrentDictionary<short, IReadOnlyList<InterceptCallback>>();
            _outgoingInterceptCallbacks = new ConcurrentDictionary<short, IReadOnlyList<InterceptCallback>>();

            Interceptor.Intercepted += Interceptor_Intercepted;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;

            if (disposing)
            {
                Interceptor.Intercepted -= Interceptor_Intercepted;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static bool IsValidParameter(ParameterInfo param)
        {
            return !param.IsOut && !param.IsIn && !param.IsOptional && !param.HasDefaultValue;
        }

        private static bool VerifyReceiveMethodSignature(MethodInfo methodInfo)
        {
            if (!methodInfo.ReturnType.Equals(typeof(void)))
                return false;

            var parameters = methodInfo.GetParameters();
            if (!parameters.All(param => IsValidParameter(param)))
                return false;

            return parameters.Length switch
            {
                0 => true,
                1 => parameters[0].ParameterType.Equals(typeof(object)) ||
                     parameters[0].ParameterType.Equals(typeof(IReadOnlyPacket)),
                2 => parameters[0].ParameterType.Equals(typeof(object)) &&
                     parameters[1].ParameterType.Equals(typeof(IReadOnlyPacket)),
                _ => false
            };
        }

        private static bool VerifyInterceptorMethodSignature(MethodInfo methodInfo)
        {
            if (!methodInfo.ReturnType.Equals(typeof(void)))
                return false;

            var parameters = methodInfo.GetParameters();
            return
                parameters.Length == 1 &&
                parameters[0].ParameterType.Equals(typeof(InterceptArgs)) &&
                parameters.All(param => IsValidParameter(param));
        }

        private void Interceptor_Intercepted(object? sender, InterceptArgs e)
        {
            if (e.IsIncoming)
                DispatchMessage(Interceptor, e.Packet);

            DispatchIntercept(e);
        }

        private void DispatchMessage(object sender, IReadOnlyPacket packet)
        {
            // Global callbacks
            if (_receiveCallbacks.TryGetValue(-1, out IReadOnlyList<ReceiveCallback>? list))
                InvokeReceiverCallbacks(list, sender, packet);

            // Message bound callbacks
            if (_receiveCallbacks.TryGetValue(packet.Header, out list))
                InvokeReceiverCallbacks(list, sender, packet);
        }

        private void InvokeReceiverCallbacks(IEnumerable<ReceiveCallback> callbacks, object sender, IReadOnlyPacket packet)
        {
            short header = packet.Header;

            foreach (var callback in callbacks)
            {
                try
                {
                    packet.Position = 0;
                    callback.Invoke(sender, packet);
                }
                catch (Exception? ex)
                {
                    if (ex is TargetInvocationException &&
                        ex.InnerException is not null)
                    {
                        ex = ex.InnerException;
                    }

                    string messageName = Messages.In.TryGetIdentifier(header, out string? name)
                        ? $"'{name}' ({header})" : $"{header}";

                    Debug.WriteLine(
                        $"Unhandled exception occurred in receiver method " +
                        $"{callback.Delegate.Target?.GetType().FullName}.{callback.Delegate.Method.Name} " +
                        $"for message {messageName}: {ex?.Message}\r\n{ex?.StackTrace}"
                    );
                }
            }
        }

        private void DispatchIntercept(InterceptArgs e)
        {
            if (GetInterceptCallbackDictionary(e.Destination)
                .TryGetValue(e.Packet.Header, out IReadOnlyList<InterceptCallback>? list))
            {
                InvokeInterceptCallbacks(list, e);
            }
        }

        private void InvokeInterceptCallbacks(IEnumerable<InterceptCallback> callbacks, InterceptArgs e)
        {
            short header = e.Packet.Header;

            foreach (var callback in callbacks)
            {
                try
                {
                    e.Packet.Position = 0;
                    callback.Invoke(e);
                }
                catch (Exception? ex)
                {
                    while (ex is TargetInvocationException && 
                        ex.InnerException is not null)
                    {
                        ex = ex.InnerException;
                    }

                    var map = e.IsOutgoing ? (HeaderDictionary)Messages.Out : Messages.In;
                    string messageName = map.TryGetIdentifier(header, out string? name) ? $"{name} ({header})" : $"{header}";

                    Debug.WriteLine(
                        $"Unhandled exception occurred in intercept method " +
                        $"{callback.Target.GetType().FullName}.{callback.Method.Name} " +
                        $"for message {messageName}: {ex.Message}\r\n{ex.StackTrace}"
                    );
                }
            }
        }

        #region - Handlers -
        public bool AddHandler(Header header, Action<object?, IReadOnlyPacket> handler)
        {
            if (handler.Target is null)
                throw new NullReferenceException("Target cannot be null on the handler delegate");

            bool result;
            IReadOnlyList<ReceiveCallback> previousList, newList;

            do
            {
                previousList = _receiveCallbacks.GetOrAdd(header, ReceiverCallbackListFactory);

                if (previousList.Any(callback => handler.Equals(callback.Delegate)))
                {
                    newList = previousList;

                    result = false;
                }
                else
                {
                    var list = previousList.ToList();
                    list.Add(new ClosedReceiveCallback(header, handler.Target, handler));
                    newList = list;

                    result = true;
                }
            }
            while (!_receiveCallbacks.TryUpdate(header, newList, previousList));

            return result;
        }

        public bool RemoveHandler(Header header, Action<object?, IReadOnlyPacket> handler)
        {
            bool result;
            IReadOnlyList<ReceiveCallback> previousList, newList;

            do
            {
                previousList = _receiveCallbacks.GetOrAdd(header, ReceiverCallbackListFactory);

                var callback = previousList.FirstOrDefault(x => handler.Equals(x.Delegate));
                if (callback == null)
                {
                    newList = previousList;

                    result = false;
                }
                else
                {
                    var list = previousList.ToList();
                    list.Remove(callback);
                    newList = list;

                    result = true;
                }
            }
            while (!_receiveCallbacks.TryUpdate(header, newList, previousList));

            return result;
        }
        #endregion

        #region - Binding -
        /// <summary>
        /// Checks if the target object is bound to the interceptor.
        /// </summary>
        public bool IsBound(object target)
        {
            return _bindings.ContainsKey(target);
        }

        /// <summary>
        /// Attempts to bind to the specified target handler.
        /// </summary>
        public void Bind(object target)
        {
            var targetType = target.GetType();
            var methodInfos = targetType.FindAllMethods();

            Identifiers
                unknownIdentifiers = new(),
                unresolvedIdentifiers = new();

            /*
                Detect unknown, invalid identifiers
            */
            {
                var classIdentifiersAttributes = targetType.GetCustomAttributes<IdentifiersAttribute>();
                foreach (var attribute in classIdentifiersAttributes)
                {
                    foreach (var identifier in attribute.Identifiers)
                    {
                        if (!Messages.IdentifierExists(identifier))
                        {
                            unknownIdentifiers.Add(identifier);
                        }
                        else if (!Messages.TryGetHeader(identifier, out Header? header) || header.Value < 0)
                        {
                            unresolvedIdentifiers.Add(identifier);
                        }
                    }
                }
            }

            foreach (var methodInfo in methodInfos)
            {
                foreach (var attribute in methodInfo.GetCustomAttributes<IdentifiersAttribute>())
                {
                    foreach (var identifier in attribute.Identifiers)
                    {
                        if (!Messages.IdentifierExists(identifier))
                        {
                            unknownIdentifiers.Add(identifier);
                        }
                        else if (!Messages.TryGetHeader(identifier, out Header? header) || header < 0)
                        {
                            unresolvedIdentifiers.Add(identifier);
                        }
                    }
                }
            }

            if (unknownIdentifiers.Any() || unresolvedIdentifiers.Any())
            {
                throw new InterceptorBindingFailedException(target, unknownIdentifiers, unresolvedIdentifiers);
            }

            var callbackList = new List<BindingCallback>();

            /*
                Generate receive/intercept callbacks
            */
            foreach (var methodInfo in methodInfos)
            {
                // Receive
                var receiveAttribute = methodInfo.GetCustomAttribute<ReceiveAttribute>();
                if (receiveAttribute != null)
                {
                    if (!VerifyReceiveMethodSignature(methodInfo))
                    {
                        throw new Exception(
                            $"{targetType.Name}.{methodInfo.Name} has a " +
                            $"method signature incompatible with {receiveAttribute.GetType().Name}"
                        );
                    }

                    if (receiveAttribute.Identifiers.Count > 0)
                    {
                        var uniqueHeaders = new HashSet<short>();
                        foreach (var identifier in receiveAttribute.Identifiers)
                        {
                            short header = Messages[identifier];
                            if (!uniqueHeaders.Add(header)) continue;

                            callbackList.Add(CreateCallback(header, target, methodInfo));
                        }
                    }
                    else
                    {
                        callbackList.Add(CreateCallback(-1, target, methodInfo));
                    }
                }

                // Intercept
                var interceptAttributes = methodInfo.GetCustomAttributes<InterceptAttribute>();
                if (interceptAttributes.Count() > 1)
                    throw new Exception($"Multiple intercept attributes defined for method {targetType.Name}.{methodInfo.Name}");

                var interceptAttribute = interceptAttributes.FirstOrDefault();
                if (interceptAttribute != null)
                {
                    if (!VerifyInterceptorMethodSignature(methodInfo))
                    {
                        throw new Exception(
                            $"{targetType.Name}.{methodInfo.Name} has a " +
                            $"method signature incompatible with {interceptAttribute.GetType().Name}"
                        );
                    }

                    if (interceptAttribute.Identifiers.Count > 0)
                    {
                        var uniqueHeaders = new HashSet<short>();
                        foreach (var identifier in interceptAttribute.Identifiers)
                        {
                            short header = Messages[identifier];
                            if (!uniqueHeaders.Add(header)) continue;

                            callbackList.Add(CreateInterceptCallback(identifier.Destination, header, target, methodInfo));
                        }
                    }
                    else
                    {
                        var destination = (interceptAttribute is InterceptOutAttribute) ? Destination.Server : Destination.Client;
                        callbackList.Add(CreateInterceptCallback(destination, -1, target, methodInfo));
                    }
                }
            }

            if (!callbackList.Any())
                throw new Exception("No attributes found to bind to");

            var binding = new InterceptorBinding(target, callbackList);

            if (!_bindings.TryAdd(target, binding))
                throw new InvalidOperationException($"The target '{targetType.FullName}' is already bound.");

            // Add receive callbacks
            foreach (var callbackGroup in callbackList.OfType<ReceiveCallback>().GroupBy(x => x.Header))
            {
                short header = callbackGroup.Key;

                IReadOnlyList<ReceiveCallback> previousList;
                List<ReceiveCallback> updatedList;

                do
                {
                    previousList = _receiveCallbacks.GetOrAdd(callbackGroup.Key, ReceiverCallbackListFactory);
                    updatedList = previousList.ToList();
                    updatedList.AddRange(callbackGroup);
                }
                while (!_receiveCallbacks.TryUpdate(callbackGroup.Key, updatedList, previousList));
            }

            // Add intercept callbacks
            foreach (var callbackGroup in callbackList.OfType<InterceptCallback>().GroupBy(x => (x.Destination, x.Header)))
            {
                short header = callbackGroup.Key.Header;

                IReadOnlyList<InterceptCallback> previousList;
                List<InterceptCallback> updatedList;

                var map = callbackGroup.Key.Destination == Destination.Server ?
                    _outgoingInterceptCallbacks : _incomingInterceptCallbacks;

                do
                {
                    previousList = map.GetOrAdd(callbackGroup.Key.Header, InterceptCallbackListFactory);
                    updatedList = previousList.ToList();
                    updatedList.AddRange(callbackGroup);
                }
                while (!map.TryUpdate(callbackGroup.Key.Header, updatedList, previousList));
            }
        }

        public bool Release(object target)
        {
            if (!_bindings.TryRemove(target, out InterceptorBinding? binding))
                return false;

            foreach (var callback in binding.Callbacks)
                callback.Unsubscribe();

            // Receivers
            foreach (var callbackGroup in binding.Callbacks.OfType<ReceiveCallback>().GroupBy(x => x.Header))
            {
                short header = callbackGroup.Key;

                IReadOnlyList<ReceiveCallback>? previousList;
                List<ReceiveCallback> newList;

                do
                {
                    if (!_receiveCallbacks.TryGetValue(header, out previousList))
                        break;

                    newList = previousList.ToList();
                    foreach (var callback in callbackGroup)
                        newList.Remove(callback);
                }
                while (!_receiveCallbacks.TryUpdate(header, newList, previousList));
            }

            // Interceptors
            foreach (var callbackGroup in binding.Callbacks.OfType<InterceptCallback>().GroupBy(x => (x.Destination, x.Header)))
            {
                short header = callbackGroup.Key.Header;

                IReadOnlyList<InterceptCallback>? previousList;
                List<InterceptCallback> newList;

                var map = callbackGroup.Key.Destination == Destination.Server ?
                    _outgoingInterceptCallbacks : _incomingInterceptCallbacks;

                do
                {
                    if (!map.TryGetValue(header, out previousList))
                        break;

                    newList = previousList.ToList();
                    foreach (var callback in callbackGroup)
                        newList.Remove(callback);
                }
                while (!map.TryUpdate(header, newList, previousList));
            }

            return true;
        }
        #endregion

        #region - Intercepts -
        public bool AddInterceptIn(Header header, Action<InterceptArgs> callback)
            => AddIntercept(Destination.Client, header, callback);

        public bool AddInterceptOut(Header header, Action<InterceptArgs> callback)
            => AddIntercept(Destination.Server, header, callback);

        public bool AddIntercept(Destination destination, Header header, Action<InterceptArgs> callback)
        {
            if (header.Value < 0)
                throw new InvalidOperationException("Invalid header specified for intercept");

            if (callback.Target is null)
                throw new InvalidOperationException("The target of the specified callback cannot be null");

            bool result;
            IReadOnlyList<InterceptCallback> previousList, newList;
            var dict = GetInterceptCallbackDictionary(destination);

            do
            {
                previousList = dict.GetOrAdd(header, InterceptCallbackListFactory);

                if (previousList.Any(x => x.Delegate.Equals(callback)))
                {
                    newList = previousList;
                    result = false;
                }
                else
                {
                    var list = previousList.ToList();
                    list.Add(new ClosedInterceptCallback(destination, header, callback.Target, callback.Method, callback));
                    newList = list;
                    result = true;
                }
            }
            while (!dict.TryUpdate(header, newList, previousList));

            return result;
        }

        public bool RemoveInterceptIn(Header header, Action<InterceptArgs> action)
            => RemoveIntercept(Destination.Client, header, action);

        public bool RemoveInterceptOut(Header header, Action<InterceptArgs> action)
            => RemoveIntercept(Destination.Server, header, action);

        public bool RemoveIntercept(Destination destination, Header header, Action<InterceptArgs> action)
        {
            bool result;
            IReadOnlyList<InterceptCallback> previousList, newList;
            var dict = GetInterceptCallbackDictionary(destination);

            do
            {
                previousList = dict.GetOrAdd(header, InterceptCallbackListFactory);

                var callback = previousList.FirstOrDefault(x => x.Delegate.Equals(action));
                if (callback != null)
                {
                    var list = previousList.ToList();
                    result = list.Remove(callback);
                    newList = list;
                }
                else
                {
                    newList = previousList;
                    result = false;
                }
            }
            while (!dict.TryUpdate(header, newList, previousList));

            return result;
        }
        #endregion
    }
}
