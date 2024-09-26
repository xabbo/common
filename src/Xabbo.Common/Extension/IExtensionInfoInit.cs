namespace Xabbo.Extension;

/// <summary>
/// Allows an extension to initialize its information.
/// The source generator will implement this interface on partial classes marked with <see cref="ExtensionAttribute"/>.
/// </summary>
public interface IExtensionInfoInit
{
    /// <summary>
    /// The extension information.
    /// </summary>
    ExtensionInfo Info { get; }
}