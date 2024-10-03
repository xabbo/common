using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Tests;

public sealed record DiagnosticInfo(
    string Id,
    string Title,
    DiagnosticSeverity Severity,
    string Location,
    string? LocationText,
    string Message
)
{
    static string GetLocationString(Location location)
    {
        var span = location.GetLineSpan();
        if (!span.IsValid)
            return "unknown";
        if (!string.IsNullOrWhiteSpace(span.Path))
            return span.ToString();
        else
            return span.ToString()[2..];
    }

    static string? GetReportedLocationText(Location location)
    {
        if (location.SourceTree is not null)
        {
            return location.SourceTree
                .GetText()
                .GetSubText(location.SourceSpan)
                .ToString();
        }
        return null;
    }

    public static DiagnosticInfo Create(Diagnostic diagnostic)
    {
        return new DiagnosticInfo(
            Id: diagnostic.Id,
            Title: diagnostic.Descriptor.Title.ToString(),
            Severity: diagnostic.Severity,
            Location: GetLocationString(diagnostic.Location),
            LocationText: GetReportedLocationText(diagnostic.Location),
            Message: diagnostic.GetMessage()
        );
    }
}