using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Xabbo.Messages;
using Xabbo.Utility;

namespace Xabbo.Interceptor.Dispatcher
{
    public class InterceptDispatcher : IInterceptDispatcher
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

        private readonly ConcurrentDictionary<object, InterceptorBinding> _bindings;
        private readonly ConcurrentDictionary<short, IReadOnlyList<ReceiveCallback>> _receiveCallbacks;
        private readonly ConcurrentDictionary<short, IReadOnlyList<InterceptCallback>> _incomingInterceptCallbacks;
        private readonly ConcurrentDictionary<short, IReadOnlyList<InterceptCallback>> _outgoingInterceptCallbacks;

        private ConcurrentDictionary<short, IReadOnlyList<InterceptCallback>>
            GetInterceptCallbackDictionary(Destination destination) =>
            destination == Destination.Server ? _outgoingInterceptCallbacks : _incomingInterceptCallbacks;

        public IMessageManager Messages { get; }

        public InterceptDispatcher(IMessageManager messages)
        {
            Messages = messages;

            _bindings = new ConcurrentDictionary<object, InterceptorBinding>();
            _receiveCallbacks = new ConcurrentDictionary<short, IReadOnlyList<ReceiveCallback>>();
            _incomingInterceptCallbacks = new ConcurrentDictionary<short, IReadOnlyList<InterceptCallback>>();
            _outgoingInterceptCallbacks = new ConcurrentDictionary<short, IReadOnlyList<InterceptCallback>>();
        }

        private static bool IsValidParameter(ParameterInfo param)
        {
            return !param.IsOut && !param.IsIn && !param.IsOptional && !param.HasDefaultValue;
        }

        private static bool VerifyReceiveMethodSignature(MethodInfo methodInfo)
        {
            if (!methodInfo.ReturnType.Equals(typeof(void)))
                return false;

            ParameterInfo[] parameters = methodInfo.GetParameters();
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

            ParameterInfo[] parameters = methodInfo.GetParameters();
            return
                parameters.Length == 1 &&
                parameters[0].ParameterType.Equals(typeof(InterceptArgs)) &&
                parameters.All(param => IsValidParameter(param));
        }

        public void DispatchMessage(object? sender, IReadOnlyPacket packet)
        {
            if (_receiveCallbacks.TryGetValue(packet.Header, out IReadOnlyList<ReceiveCallback>? list))
                InvokeReceiverCallbacks(list, sender, packet);
        }

        private void InvokeReceiverCallbacks(IEnumerable<ReceiveCallback> callbacks, object? sender, IReadOnlyPacket packet)
        {
            short header = packet.Header;

            foreach (ReceiveCallback callback in callbacks)
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

        public void DispatchIntercept(InterceptArgs e)
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

            foreach (InterceptCallback callback in callbacks)
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

                    HeaderDictionary map = e.IsOutgoing ? (HeaderDictionary)Messages.Out : Messages.In;
                    string messageName = map.TryGetIdentifier(header, out string? name) ? $"{name} ({header})" : $"{header}";

                    Debug.WriteLine(
                        $"Unhandled exception occurred in intercept method " +
                        $"{callback.Target.GetType().FullName}.{callback.Method.Name} " +
                        $"for message {messageName}: {ex.Message}\r\n{ex.StackTrace}"
                    );
                }
            }
        }

        public void ReleaseAll()
        {
            _bindings.Clear();
            _receiveCallbacks.Clear();
            _incomingInterceptCallbacks.Clear();
            _outgoingInterceptCallbacks.Clear();
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
                    List<ReceiveCallback> list = previousList.ToList();
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

                ReceiveCallback? callback = previousList.FirstOrDefault(x => handler.Equals(x.Delegate));
                if (callback == null)
                {
                    newList = previousList;

                    result = false;
                }
                else
                {
                    List<ReceiveCallback> list = previousList.ToList();
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
            Type targetType = target.GetType();
            MethodInfo[] methodInfos = targetType.FindAllMethods().ToArray();

            Identifiers
                unknownIdentifiers = new(),
                unresolvedIdentifiers = new();

            /*
                Detect unknown, invalid identifiers
            */
            {
                foreach (IdentifiersAttribute attribute in targetType.GetCustomAttributes<IdentifiersAttribute>())
                {
                    foreach (Identifier identifier in attribute.Identifiers)
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

            foreach (MethodInfo methodInfo in methodInfos)
            {
                foreach (IdentifiersAttribute attribute in methodInfo.GetCustomAttributes<IdentifiersAttribute>())
                {
                    foreach (Identifier identifier in attribute.Identifiers)
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

            List<BindingCallback> callbackList = new();

            /*
                Generate receive/intercept callbacks
            */
            foreach (MethodInfo methodInfo in methodInfos)
            {
                // Receive
                ReceiveAttribute? receiveAttribute = methodInfo.GetCustomAttribute<ReceiveAttribute>();
                if (receiveAttribute != null)
                {
                    if (!VerifyReceiveMethodSignature(methodInfo))
                    {
                        throw new Exception(
                            $"{targetType.Name}.{methodInfo.Name} has a " +
                            $"method signature incompatible with {receiveAttribute.GetType().Name}"
                        );
                    }

                    HashSet<short> uniqueHeaders = new();
                    foreach (Identifier identifier in receiveAttribute.Identifiers)
                    {
                        short header = Messages[identifier];
                        if (!uniqueHeaders.Add(header)) continue;

                        callbackList.Add(CreateCallback(header, target, methodInfo));
                    }
                }

                // Intercept
                IEnumerable<InterceptAttribute> interceptAttributes = methodInfo.GetCustomAttributes<InterceptAttribute>();
                if (interceptAttributes.Count() > 1)
                    throw new Exception($"Multiple intercept attributes defined for method {targetType.Name}.{methodInfo.Name}");

                InterceptAttribute? interceptAttribute = interceptAttributes.FirstOrDefault();
                if (interceptAttribute != null)
                {
                    if (!VerifyInterceptorMethodSignature(methodInfo))
                    {
                        throw new Exception(
                            $"{targetType.Name}.{methodInfo.Name} has a " +
                            $"method signature incompatible with {interceptAttribute.GetType().Name}"
                        );
                    }

                    HashSet<short> uniqueHeaders = new();
                    foreach (Identifier identifier in interceptAttribute.Identifiers)
                    {
                        short header = Messages[identifier];
                        if (!uniqueHeaders.Add(header)) continue;

                        callbackList.Add(CreateInterceptCallback(identifier.Destination, header, target, methodInfo));
                    }
                }
            }

            if (!callbackList.Any())
                throw new Exception("No attributes found to bind to");

            InterceptorBinding binding = new(target, callbackList);

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

            foreach (BindingCallback callback in binding.Callbacks)
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
                    foreach (ReceiveCallback callback in callbackGroup)
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
                    foreach (InterceptCallback callback in callbackGroup)
                        newList.Remove(callback);
                }
                while (!map.TryUpdate(header, newList, previousList));
            }

            return true;
        }
        #endregion

        #region - Intercepts -
        public void AddInterceptIn(Header header, Action<InterceptArgs> callback)
            => AddIntercept(Destination.Client, header, callback);

        public void AddInterceptOut(Header header, Action<InterceptArgs> callback)
            => AddIntercept(Destination.Server, header, callback);

        public void AddIntercept(Destination destination, Header header, Action<InterceptArgs> callback)
        {
            if (header.Value < 0)
                throw new InvalidOperationException("Invalid header specified for intercept");

            if (callback.Target is null)
                throw new InvalidOperationException("The target of the specified callback cannot be null");

            IReadOnlyList<InterceptCallback> previousList, newList;
            var dict = GetInterceptCallbackDictionary(destination);

            do
            {
                previousList = dict.GetOrAdd(header, InterceptCallbackListFactory);

                if (previousList.Any(x => x.Delegate.Equals(callback)))
                {
                    throw new InvalidOperationException("The specified intercept callback has already been added.");
                }
                else
                {
                    List<InterceptCallback> list = previousList.ToList();
                    list.Add(new ClosedInterceptCallback(destination, header, callback.Target, callback.Method, callback));
                    newList = list;
                }
            }
            while (!dict.TryUpdate(header, newList, previousList));
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

                InterceptCallback? callback = previousList.FirstOrDefault(x => x.Delegate.Equals(action));
                if (callback != null)
                {
                    List<InterceptCallback> list = previousList.ToList();
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
