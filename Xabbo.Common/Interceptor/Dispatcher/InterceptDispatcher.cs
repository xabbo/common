using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Xabbo.Common;
using Xabbo.Messages;
using Xabbo.Messages.Attributes;
using Xabbo.Utility;

namespace Xabbo.Interceptor.Dispatcher
{
    /// <inheritdoc cref="IInterceptDispatcher" />
    public class InterceptDispatcher : IInterceptDispatcher
    {
        private static ReceiveCallback CreateCallback(Header header, object target, MethodInfo method)
        {
            var callback = ReceiveDelegateFactory.GetOpenDelegate(method);
            return new OpenReceiveCallback(header, target, method, callback);
        }

        private static InterceptCallback CreateInterceptCallback(Header header, object target, MethodInfo method)
        {
            var callback = InterceptDelegateFactory.GetOpenDelegate(method);
            return new OpenInterceptCallback(header, target, method, callback);
        }

        private static IReadOnlyList<ReceiveCallback> ReceiverCallbackListFactory(ClientHeader key)
            => new List<ReceiveCallback>();

        private static IReadOnlyList<InterceptCallback> InterceptCallbackListFactory(ClientHeader key)
            => new List<InterceptCallback>();

        private readonly ConcurrentDictionary<IInterceptHandler, InterceptorBinding> _bindings = new();
        private readonly ConcurrentDictionary<ClientHeader, IReadOnlyList<ReceiveCallback>> _receiveCallbacks = new();
        private readonly ConcurrentDictionary<ClientHeader, IReadOnlyList<InterceptCallback>> _interceptCallbacks = new();

        /// <summary>
        /// Gets the message manager used by this dispatcher.
        /// </summary>
        public IMessageManager Messages { get; }

        /// <summary>
        /// Creates a new <see cref="InterceptDispatcher"/> using the specified <see cref="IMessageManager"/>.
        /// </summary>
        /// <param name="messages"></param>
        public InterceptDispatcher(IMessageManager messages)
        {
            Messages = messages;
        }

        private static bool IsValidParameter(ParameterInfo param)
        {
            return !param.IsOut && !param.IsIn && !param.IsOptional && !param.HasDefaultValue;
        }

        private static bool ValidateReceiveMethodSignature(MethodInfo methodInfo)
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

        private static bool ValidateInterceptorMethodSignature(MethodInfo methodInfo)
        {
            if (!methodInfo.ReturnType.Equals(typeof(void)))
                return false;

            ParameterInfo[] parameters = methodInfo.GetParameters();
            return
                parameters.Length == 1 &&
                parameters[0].ParameterType.Equals(typeof(InterceptArgs)) &&
                parameters.All(param => IsValidParameter(param));
        }

        /// <summary>
        /// Dispatches the specified message to all bound receive callbacks.
        /// </summary>
        public void DispatchMessage(object? sender, IReadOnlyPacket packet)
        {
            ClientHeader? clientHeader = packet.Header.GetClientHeader(packet.Protocol);
            if (clientHeader is null) return;

            if (_receiveCallbacks.TryGetValue(clientHeader, out IReadOnlyList<ReceiveCallback>? list))
                InvokeReceiverCallbacks(list, sender, packet);
        }

        private void InvokeReceiverCallbacks(IEnumerable<ReceiveCallback> callbacks, object? sender, IReadOnlyPacket packet)
        {
            ClientHeader? header = packet.Header.GetClientHeader(packet.Protocol);
            if (header is null) return;

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

                    string messageName = Messages.In.TryGetName(packet.Protocol, header.Value, out string? name)
                        ? $"'{name}' ({header})" : $"{header}";

                    Debug.WriteLine(
                        $"Unhandled exception occurred in receiver method " +
                        $"{callback.Method.DeclaringType?.Name}.{callback.Method.Name} " +
                        $"for message {messageName}: {ex?.Message}\r\n{ex?.StackTrace}"
                    );
                }
            }
        }

        /// <summary>
        /// Dispatches the specified intercept arguments to all bound intercept callbacks.
        /// </summary>
        public void DispatchIntercept(InterceptArgs e)
        {
            ClientHeader? key = e.Packet.Header.GetClientHeader(e.Packet.Protocol);
            if (key is null) return;

            if (_interceptCallbacks.TryGetValue(key, out IReadOnlyList<InterceptCallback>? list))
            {
                InvokeInterceptCallbacks(list, e);
            }
        }

        private void InvokeInterceptCallbacks(IEnumerable<InterceptCallback> callbacks, InterceptArgs e)
        {
            ClientHeader? header = e.Packet.Header.GetClientHeader(e.Packet.Protocol);
            if (header is null) return;

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

                    string messageName = header.Value.ToString();
                    if (Messages.TryGetInfoByHeader(e.Destination.ToDirection(),
                        e.Packet.Protocol, header.Value, out MessageInfo? messageInfo))
                    {
                        messageName = messageInfo.UnityName ?? messageInfo.FlashName ?? messageName;
                    }

                    Debug.WriteLine(
                        $"Unhandled exception occurred in intercept method " +
                        $"{callback.Method.DeclaringType?.Name}.{callback.Method.Name} " +
                        $"for message {messageName}: {ex.Message}\r\n{ex.StackTrace}"
                    );
                }
            }
        }

        /// <summary>
        /// Releases all bindings, intercepts and receive callbacks.
        /// </summary>
        public void ReleaseAll()
        {
            _bindings.Clear();
            _receiveCallbacks.Clear();
            _interceptCallbacks.Clear();
        }

        #region - Handlers -
        private bool AddHandler(Header header, ClientHeader key, Action<object?, IReadOnlyPacket> handler)
        {
            if (handler.Target is null)
                throw new NullReferenceException("Target cannot be null on the handler delegate");

            bool result;
            IReadOnlyList<ReceiveCallback> previousList, newList;

            do
            {
                previousList = _receiveCallbacks.GetOrAdd(key, ReceiverCallbackListFactory);

                if (previousList.Any(callback => handler.Equals(callback.Delegate)))
                {
                    newList = previousList;
                    result = false;
                }
                else
                {
                    List<ReceiveCallback> list = previousList.ToList();
                    list.Add(new ClosedReceiveCallback(header, handler.Target, handler.Method, handler));
                    newList = list;

                    result = true;
                }
            }
            while (!_receiveCallbacks.TryUpdate(key, newList, previousList));

            return result;
        }

        public bool AddHandler(Header header, Action<object?, IReadOnlyPacket> handler)
        {
            bool modified = false;
            modified |= header.Flash is not null && AddHandler(header, header.Flash, handler);
            modified |= header.Unity is not null && AddHandler(header, header.Unity, handler);
            return modified;
        }

        private bool RemoveHandler(ClientHeader key, Action<object?, IReadOnlyPacket> handler)
        {
            bool result;
            IReadOnlyList<ReceiveCallback> previousList, newList;

            do
            {
                previousList = _receiveCallbacks.GetOrAdd(key, ReceiverCallbackListFactory);

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
            while (!_receiveCallbacks.TryUpdate(key, newList, previousList));

            return result;
        }

        public bool RemoveHandler(Header header, Action<object?, IReadOnlyPacket> handler)
        {
            bool modified = false;
            modified |= header.Flash is not null && RemoveHandler(header.Flash, handler);
            modified |= header.Unity is not null && RemoveHandler(header.Unity, handler);
            return modified;
        }
        #endregion

        #region - Binding -
        private void AddReceiveCallbacks(ClientHeader key, IEnumerable<ReceiveCallback> callbacks)
        {
            IReadOnlyList<ReceiveCallback> previousList;
            List<ReceiveCallback> updatedList;

            do
            {
                previousList = _receiveCallbacks.GetOrAdd(key, ReceiverCallbackListFactory);
                updatedList = previousList.ToList();
                updatedList.AddRange(callbacks);
            }
            while (!_receiveCallbacks.TryUpdate(key, updatedList, previousList));
        }

        private void AddReceiveCallbacks(Header header, IEnumerable<ReceiveCallback> callbacks)
        {
            if (header.Flash is not null) AddReceiveCallbacks(header.Flash, callbacks);
            if (header.Unity is not null) AddReceiveCallbacks(header.Unity, callbacks);
        }

        private void AddInterceptCallbacks(ClientHeader key, IEnumerable<InterceptCallback> callbacks)
        {
            IReadOnlyList<InterceptCallback> previousList;
            List<InterceptCallback> updatedList;

            do
            {
                previousList = _interceptCallbacks.GetOrAdd(key, InterceptCallbackListFactory);
                updatedList = previousList.ToList();
                updatedList.AddRange(callbacks);
            }
            while (!_interceptCallbacks.TryUpdate(key, updatedList, previousList));
        }

        private void AddInterceptCallbacks(Header header, IEnumerable<InterceptCallback> callbacks)
        {
            if (header.Flash is not null) AddInterceptCallbacks(header.Flash, callbacks);
            if (header.Unity is not null) AddInterceptCallbacks(header.Unity, callbacks);
        }

        public bool IsBound(IInterceptHandler handler)
        {
            return _bindings.ContainsKey(handler);
        }

        public bool Bind(IInterceptHandler handler, ClientType requiredClientHeaders = ClientType.Flash | ClientType.Unity)
        {
            Type handlerType = handler.GetType();
            MethodInfo[] methods = handlerType.FindAllMethods().ToArray();

            Identifiers
                unknownIdentifiers = new(),
                unresolvedIdentifiers = new();

            /*
                Detect unknown, invalid identifiers
            */
            {
                foreach (IdentifiersAttribute attribute in handlerType.GetCustomAttributes<IdentifiersAttribute>())
                {
                    if (!attribute.Required) continue;
                    if ((attribute.RequiredClient & requiredClientHeaders) == 0) continue;

                    foreach (Identifier identifier in attribute.Identifiers)
                    {
                        if (!Messages.IdentifierExists(identifier))
                        {
                            unknownIdentifiers.Add(identifier);
                        }
                        else if (!Messages.TryGetHeader(identifier, out Header? header))
                        {
                            unresolvedIdentifiers.Add(identifier);
                        }
                        else if ((requiredClientHeaders.HasFlag(ClientType.Flash) && !(header.Flash?.Value >= 0)) ||
                                (requiredClientHeaders.HasFlag(ClientType.Unity) && !(header.Unity?.Value >= 0)))
                        {
                            unresolvedIdentifiers.Add(identifier);
                        }
                    }
                }
            }

            foreach (MethodInfo methodInfo in methods)
            {
                foreach (IdentifiersAttribute attribute in methodInfo.GetCustomAttributes<IdentifiersAttribute>())
                {
                    if (!attribute.Required) continue;
                    if ((attribute.RequiredClient & requiredClientHeaders) == 0) continue;

                    foreach (Identifier identifier in attribute.Identifiers)
                    {
                        if (!Messages.IdentifierExists(identifier))
                        {
                            unknownIdentifiers.Add(identifier);
                        }
                        else if (!Messages.TryGetHeader(identifier, out Header? header))
                        {
                            unresolvedIdentifiers.Add(identifier);
                        }
                        else if ((requiredClientHeaders.HasFlag(ClientType.Flash) && !(header.Flash?.Value >= 0)) ||
                                 (requiredClientHeaders.HasFlag(ClientType.Unity) && !(header.Unity?.Value >= 0)))
                        {
                            unresolvedIdentifiers.Add(identifier);
                        }
                    }
                }
            }

            if (unknownIdentifiers.Any() || unresolvedIdentifiers.Any())
            {
                throw new InterceptorBindingFailedException(handler, unknownIdentifiers, unresolvedIdentifiers);
            }

            List<BindingCallback> callbackList = new();

            /*
                Generate receive/intercept callbacks
            */
            foreach (MethodInfo methodInfo in methods)
            {
                // Receive
                ReceiveAttribute? receiveAttribute = methodInfo.GetCustomAttribute<ReceiveAttribute>();
                if (receiveAttribute != null)
                {
                    if (!ValidateReceiveMethodSignature(methodInfo))
                    {
                        throw new Exception(
                            $"{handlerType.Name}.{methodInfo.Name} has a " +
                            $"method signature incompatible with {receiveAttribute.GetType().Name}"
                        );
                    }

                    HashSet<Header> uniqueHeaders = new();
                    foreach (Identifier identifier in receiveAttribute.Identifiers)
                    {
                        Header header = Messages[identifier];
                        if (!uniqueHeaders.Add(header)) continue;

                        callbackList.Add(CreateCallback(header, handler, methodInfo));
                    }
                }

                // Intercept
                IEnumerable<InterceptAttribute> interceptAttributes = methodInfo.GetCustomAttributes<InterceptAttribute>();
                if (interceptAttributes.Count() > 1)
                    throw new Exception($"Multiple intercept attributes defined for method {handlerType.Name}.{methodInfo.Name}");

                InterceptAttribute? interceptAttribute = interceptAttributes.FirstOrDefault();
                if (interceptAttribute != null)
                {
                    if (!ValidateInterceptorMethodSignature(methodInfo))
                    {
                        throw new Exception(
                            $"{handlerType.Name}.{methodInfo.Name} has a " +
                            $"method signature incompatible with {interceptAttribute.GetType().Name}"
                        );
                    }

                    HashSet<Header> uniqueHeaders = new();
                    foreach (Identifier identifier in interceptAttribute.Identifiers)
                    {
                        Header header = Messages[identifier];
                        if (!uniqueHeaders.Add(header)) continue;

                        callbackList.Add(CreateInterceptCallback(header, handler, methodInfo));
                    }
                }
            }

            if (!callbackList.Any())
            {
                return false;
            }

            InterceptorBinding binding = new(handler, callbackList);

            if (!_bindings.TryAdd(handler, binding))
                throw new InvalidOperationException($"The target '{handlerType.FullName}' is already bound.");

            // Add receive callbacks
            foreach (var callbacks in callbackList.OfType<ReceiveCallback>().GroupBy(x => x.Header))
            {
                AddReceiveCallbacks(callbacks.Key, callbacks);
            }

            // Add intercept callbacks
            foreach (var callbacks in callbackList.OfType<InterceptCallback>().GroupBy(x => x.Header))
            {
                AddInterceptCallbacks(callbacks.Key, callbacks);
            }

            return true;
        }

        private void RemoveReceiveCallbacks(ClientHeader key,
            IEnumerable<ReceiveCallback> callbacks)
        {
            IReadOnlyList<ReceiveCallback>? previousList;
            List<ReceiveCallback> newList;

            do
            {
                if (!_receiveCallbacks.TryGetValue(key, out previousList))
                    break;

                newList = previousList.ToList();
                foreach (ReceiveCallback callback in callbacks)
                    newList.Remove(callback);
            }
            while (!_receiveCallbacks.TryUpdate(key, newList, previousList));
        }

        private void RemoveReceiveCallbacks(Header header, IEnumerable<ReceiveCallback> callbacks)
        {
            if (header.Flash is not null) RemoveReceiveCallbacks(header.Flash, callbacks);
            if (header.Unity is not null) RemoveReceiveCallbacks(header.Unity, callbacks);
        }

        private void RemoveInterceptCallbacks(ClientHeader key, IEnumerable<InterceptCallback> callbacks)
        {
            IReadOnlyList<InterceptCallback>? previousList;
            List<InterceptCallback> newList;

            do
            {
                if (!_interceptCallbacks.TryGetValue(key, out previousList))
                    break;

                newList = previousList.ToList();
                foreach (InterceptCallback callback in callbacks)
                    newList.Remove(callback);
            }
            while (!_interceptCallbacks.TryUpdate(key, newList, previousList));
        }

        private void RemoveInterceptCallbacks(Header header, IEnumerable<InterceptCallback> callbacks)
        {
            if (header.Flash is not null) RemoveInterceptCallbacks(header.Flash, callbacks);
            if (header.Unity is not null) RemoveInterceptCallbacks(header.Unity, callbacks);
        }

        public bool Release(IInterceptHandler handler)
        {
            if (!_bindings.TryRemove(handler, out InterceptorBinding? binding))
                return false;

            foreach (BindingCallback callback in binding.Callbacks)
            {
                callback.Unsubscribe();
            }

            // Receivers
            foreach (var callbacks in binding.Callbacks.OfType<ReceiveCallback>().GroupBy(x => x.Header))
            {
                RemoveReceiveCallbacks(callbacks.Key, callbacks);
            }

            // Intercepts
            foreach (var callbacks in binding.Callbacks.OfType<InterceptCallback>().GroupBy(x => x.Header))
            {
                RemoveInterceptCallbacks(callbacks.Key, callbacks);
            }

            return true;
        }
        #endregion

        private static bool CheckClientHeader(ClientType requiredClientHeaders, Header header)
        {
            if (header.Value.HasValue) return true;

            if (requiredClientHeaders.HasFlag(ClientType.Flash) &&
                !(header.Flash?.Value >= 0))
            {
                return false;
            }

            if (requiredClientHeaders.HasFlag(ClientType.Unity) &&
                !(header.Unity?.Value >= 0))
            {
                return false;
            }

            return true;
        }

        #region - Intercepts -
        private void AddIntercept(Header header, ClientHeader key, Action<InterceptArgs> callback)
        {
            IReadOnlyList<InterceptCallback> previousList, newList;

            do
            {
                previousList = _interceptCallbacks.GetOrAdd(key, InterceptCallbackListFactory);

                if (previousList.Any(x => x.Delegate.Equals(callback)))
                {
                    throw new InvalidOperationException("The specified intercept callback has already been added.");
                }
                else
                {
                    List<InterceptCallback> list = previousList.ToList();
                    list.Add(new ClosedInterceptCallback(header, callback.Target, callback.Method, callback));
                    newList = list;
                }
            }
            while (!_interceptCallbacks.TryUpdate(key, newList, previousList));
        }

        public void AddIntercept(HeaderSet headers, Action<InterceptArgs> callback, ClientType requiredClientHeaders)
        {
            foreach (Header header in headers)
            {
                if (!CheckClientHeader(requiredClientHeaders, header))
                    throw new InvalidOperationException("Invalid header specified for intercept.");
            }

            if (callback.Target is null)
                throw new InvalidOperationException("The target of the specified callback cannot be null.");

            foreach (Header header in headers)
            {
                if (header.Flash is not null) AddIntercept(header, header.Flash, callback);
                if (header.Unity is not null) AddIntercept(header, header.Unity, callback);
            }
        }

        public bool RemoveInterceptIn(Header header, Action<InterceptArgs> action)
            => RemoveIntercept(header, action);

        public bool RemoveInterceptOut(Header header, Action<InterceptArgs> action)
            => RemoveIntercept(header, action);

        private bool RemoveIntercept(ClientHeader key, Action<InterceptArgs> action)
        {
            bool result;
            IReadOnlyList<InterceptCallback> previousList, newList;

            do
            {
                previousList = _interceptCallbacks.GetOrAdd(key, InterceptCallbackListFactory);

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
            while (!_interceptCallbacks.TryUpdate(key, newList, previousList));

            return result;
        }

        public bool RemoveIntercept(Header header, Action<InterceptArgs> callback)
        {
            bool modified = false;
            modified |= header.Flash is not null && RemoveIntercept(header.Flash, callback);
            modified |= header.Unity is not null && RemoveIntercept(header.Unity, callback);
            return modified;
        }
        #endregion
    }
}
