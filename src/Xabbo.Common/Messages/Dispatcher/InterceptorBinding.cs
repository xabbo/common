using System.Collections.Generic;

namespace Xabbo.Messages.Dispatcher;

internal sealed class InterceptorBinding(IMessageHandler handler, IEnumerable<BindingCallback> callbacks)
{
    public IMessageHandler Handler { get; } = handler;
    public IReadOnlyCollection<BindingCallback> Callbacks { get; } = new List<BindingCallback>(callbacks).AsReadOnly();
}
