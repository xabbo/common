using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Model;

internal sealed record DiagnosticInfo
{
    public DiagnosticInfo(DiagnosticDescriptor descriptor, Location? location, params string[] args)
    {
        Descriptor = descriptor;
        Location = location is not null ? LocationInfo.CreateFrom(location) : null;
        Args = args;
    }

    public DiagnosticDescriptor Descriptor { get; }
    public LocationInfo? Location { get; }
    public EquatableArray<string> Args { get; }
}
