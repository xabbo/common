namespace Xabbo;

/// <summary>
/// Provides event arguments for the <see cref="Interceptor.IInterceptor.Initialized"/> event.
/// </summary>
/// <param name="IsGameConnected">
/// Gets whether the game is already connected at the time of extension initialization.
/// </param>
public readonly record struct InitializedArgs(bool? IsGameConnected = null);
