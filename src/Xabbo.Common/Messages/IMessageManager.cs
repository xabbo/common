using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Xabbo.Messages;

/// <summary>
/// Represents a service that manages client specific message &amp; header information.
/// </summary>
public interface IMessageManager
{
    /// <summary>
    /// Initializes the message manager.
    /// </summary>
    Task InitializeAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Loads the specified client messages.
    /// </summary>
    void LoadMessages(IEnumerable<ClientMessage> messages);

    /// <summary>
    /// Attempts to get a header by its identifier.
    /// </summary>
    bool TryGetHeader(Identifier identifier, out Header header);

    /// <summary>
    /// Attempts to get the associated message names for the specified identifier.
    /// </summary>
    bool TryGetNames(Identifier identifier, out MessageNames names);

    /// <summary>
    /// Attempts to get the associated message names for the specified header.
    /// </summary>
    bool TryGetNames(Header header, out MessageNames names);

    /// <summary>
    /// Resolves the specified identifier to a header.
    /// </summary>
    /// <exception cref="UnresolvedIdentifiersException">If the identifier could not be resolved.</exception>
    Header Resolve(Identifier identifier);

    /// <summary>
    /// Resolves the specified identifiers to an array of headers.
    /// </summary>
    /// <exception cref="ArgumentException">If no identifiers were specified.</exception>
    /// <exception cref="UnresolvedIdentifiersException">If any of the identifiers could not be resolved.</exception>
    Header[] Resolve(ReadOnlySpan<Identifier> identifiers);

    /// <summary>
    /// Gets whether the header matches any of the specified identifiers.
    /// </summary>
    bool Is(Header header, ReadOnlySpan<Identifier> identifiers);
}
