//HintName: global.Context.InterceptorContext.g.cs
partial class Context
{
    // Generating 10 send header method(s)
    
    private void Send<T>(global::Xabbo.Messages.Header header, T arg){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T>(arg);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2>(global::Xabbo.Messages.Header header, T1 arg1, T2 arg2){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3>(global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4>(global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5>(global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5, T6>(global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5, T6, T7>(global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5, T6, T7, T8>(global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9>(global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        packet.Write<T9>(arg9);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        packet.Write<T9>(arg9);
        packet.Write<T10>(arg10);
        context.Interceptor.Send(packet);
    }
    // Generating 10 send identifier method(s)
    
    private void Send<T>(global::Xabbo.Messages.Identifier identifier, T arg){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T>(arg);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2>(global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3>(global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4>(global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5>(global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5, T6>(global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5, T6, T7>(global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5, T6, T7, T8>(global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9>(global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        packet.Write<T9>(arg9);
        context.Interceptor.Send(packet);
    }
    
    private void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10){
        global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;
        global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        packet.Write<T9>(arg9);
        packet.Write<T10>(arg10);
        context.Interceptor.Send(packet);
    }
}
