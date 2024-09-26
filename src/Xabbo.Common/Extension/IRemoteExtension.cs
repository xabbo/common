using System.Threading;
using System.Threading.Tasks;

namespace Xabbo.Extension;

/// <summary>
/// Represents an extension interface provided by a remote packet interceptor service.
/// </summary>
public interface IRemoteExtension : IExtension
{
    /// <summary>
    /// Gets the port that is currently being used to connect to the remote interceptor.
    /// </summary>
    int Port { get; }

    /// <summary>
    /// Connects to the remote interceptor and runs the packet processing loop.
    /// </summary>
    Task RunAsync(CancellationToken cancellationToken = default);
}
