using System;
using System.Collections.Generic;
using System.Threading;

namespace Xabbo.Messages;

internal sealed class DispatchList
{
    private sealed class Registration(DispatchList list, InterceptHandler handler) : IDisposable
    {
        internal readonly DispatchList _list = list;
        internal readonly InterceptHandler _handler = handler;
        internal volatile bool _deregistered;
        public void Dispose()
        {
            _deregistered = true;
            _list.TryRemove(this);
        }
    }

    private readonly object _lock = new();
    private readonly List<Registration> _registrations = [];

    public IDisposable Register(InterceptHandler handler)
    {
        if (Monitor.IsEntered(_lock))
            throw new Exception("Failed to add intercept handler.");

        lock (_lock)
        {
            var reg = new Registration(this, handler);
            _registrations.Add(reg);
            return reg;
        }
    }

    private void TryRemove(Registration reg)
    {
        // If not removed now, it should be pruned on next dispatch.
        if (!Monitor.IsEntered(_lock))
        {
            lock (_lock) _registrations.Remove(reg);
        }
    }

    public void Dispatch(Intercept intercept)
    {
        lock (_lock)
        {
            int keep = 0;
            for (int i = 0; i < _registrations.Count; i++)
            {
                if (_registrations[i]._deregistered) {
                    continue;
                }
                intercept.Packet.Position = 0;
                _registrations[i]._handler.Callback.Invoke(intercept);
                if (!_registrations[i]._deregistered) {
                    if (keep < i)
                        _registrations[keep] = _registrations[i];
                    keep++;
                }
            }
            if (keep < _registrations.Count)
                _registrations.RemoveRange(keep, _registrations.Count - keep);
        }
    }

    public void Clear()
    {
        lock (_lock)
        {
            _registrations.Clear();
        }
    }
}
