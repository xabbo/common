namespace Xabbo;

/// <summary>
/// Provides data for the <see cref="Interceptor.IInterceptor.Initialized"/> event.
/// </summary>
/// <param name="IsGameConnected">
/// Gets whether the game is already connected at the time of extension initialization.
/// </param>
public sealed record InitializedEventArgs(bool? IsGameConnected = null);
