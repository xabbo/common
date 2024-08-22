using Microsoft.CodeAnalysis;

using Xabbo.Common.Generator.Model;

namespace Xabbo.Common.Generator;

internal static partial class Executor
{
    public static void ReportDiagnostics(SourceProductionContext context, EquatableArray<DiagnosticInfo> diagnostics)
    {
        foreach (var diagnostic in diagnostics)
            ReportDiagnostic(context, diagnostic);
    }

    public static void ReportDiagnostic(SourceProductionContext context, DiagnosticInfo diagnostic)
    {
        context.ReportDiagnostic(Diagnostic.Create(
            diagnostic.Descriptor,
            diagnostic.Location?.ToLocation(),
            diagnostic.Args.ToArray()
        ));
    }
}
