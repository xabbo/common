namespace Xabbo.Extension;

/// <summary>
/// Provides data for the <see cref="IExtension.Initialized"/> event.
/// </summary>
/// <param name="IsGameConnected">
/// Gets whether the game is already connected at the time of extension initialization.
/// </param>
public readonly record struct InitializedArgs(bool? IsGameConnected = null);
