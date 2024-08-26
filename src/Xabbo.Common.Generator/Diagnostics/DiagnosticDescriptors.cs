using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Diagnostics;

internal static class DiagnosticDescriptors
{
    internal static readonly DiagnosticDescriptor InvalidMessageIdentifier = new(
        id: "XABBO001",
        title: "Invalid message identifier",
        messageFormat: "'{0}' is not a valid message identifier",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor InvalidSingleClientSpecifier = new(
        id: "XABBO002",
        title: "Invalid single client specifier",
        messageFormat: "'{0}' is not a valid client specifier. Use 'u:', 'f:' or 's:' to specify the Unity, Flash or Shockwave clients.",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor IdentifiersNotSpecified = new(
        id: "XABBO003",
        title: "No identifiers specified",
        messageFormat: "At least one message identifier must be specified",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor InterceptorMustBePartial = new(
        id: "XABBO004",
        title: "Intercept handler must be marked partial",
        messageFormat: "The intercept handler class must be marked as partial",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor InterceptHandlerMustNotBeStatic = new(
        id: "XABBO005",
        title: "Intercept handler method must not be static",
        messageFormat: "The intercept handler method must not be static",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor NoTargetClientsSpecified = new(
        id: "XABBO006",
        title: "No target clients specified",
        messageFormat: "No target clients were specified, and this interceptor will have no effect",
        category: "Xabbo",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor EmptyTargetClient = new(
        id: "XABBO007",
        title: "Empty target client",
        messageFormat: "The specified target client results in ClientType.None, and this interceptor will never be registered",
        category: "Xabbo",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
}