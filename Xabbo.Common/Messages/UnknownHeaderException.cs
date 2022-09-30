using System;

namespace Xabbo.Messages;

/// <summary>
/// Thrown when attempting to access a named header that does not exist.
/// </summary>
public sealed class UnknownHeaderException : Exception
{
    private static string BuildMessage(Identifier identifier)
    {
        return $"Unknown {(identifier.IsIncoming ? "incoming" : "outgoing")} header: \"{identifier.Name}\".";
    }

    /// <summary>
    /// The identifier of the header.
    /// </summary>
    public Identifier Identifier { get; }

    internal UnknownHeaderException(Identifier identifier)
        : base(BuildMessage(identifier))
    {
        Identifier = identifier;
    }
}
