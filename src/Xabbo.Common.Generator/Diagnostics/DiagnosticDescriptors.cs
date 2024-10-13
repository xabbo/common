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

    internal static readonly DiagnosticDescriptor NoTargetClientsOnInterceptAttribute = new(
        id: "XABBO006",
        title: "The Intercepts attribute does not specify a target client",
        messageFormat: "This attribute will have no effect because no target clients were specified",
        category: "Xabbo",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor EmptyTargetClientOnInterceptsAttribute = new(
        id: "XABBO007",
        title: "The Intercepts attribute's target client is None",
        messageFormat: "This interceptor will never be registered because the specified target client results in ClientType.None",
        category: "Xabbo",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor InvalidInterceptMethodSignature = new(
        id: "XABBO008",
        title: "Invalid intercept method signature",
        messageFormat: "Intercept method signature must be void(Intercept)",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor NotPrimitiveType = new(
        id: "XABBO010",
        title: "Not a primitive packet type",
        messageFormat: "'{0}' is not a primitive packet type",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor NotPrimitiveOrParserType = new(
        id: "XABBO011",
        title: "The type must be a packet primitive type or implement the IParser<T> interface",
        messageFormat: "'{0}' is not a packet primitive or IParser<T> implementation",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor NotPrimitiveOrComposerType = new(
        id: "XABBO012",
        title: "The type must be a packet primitive type or implement the IComposer interface",
        messageFormat: "'{0}' is not a packet primitive or IComposer implementation",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor NotPrimitiveOrParserComposerType = new(
        id: "XABBO013",
        title: "The type must be a packet primitive type or implement the IParserComposer<T> interface",
        messageFormat: "'{0}' is not a packet primitive or IParserComposer<T> implementation",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor DirectionalInterceptOnMessageHandler = new(
        id: "XABBO014",
        title: "Message intercept handlers should not specify directional intercept attributes",
        messageFormat: "Message intercept handlers should not specify directional intercept attributes",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor ClientSpecifierOnMessageHandler = new(
        id: "XABBO015",
        title: "Message intercept handlers should not specify a target client",
        messageFormat: "Message intercept handlers should not specify a target client",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor InvalidMessageHandlerSignature = new(
        id: "XABBO016",
        title: "Invalid message handler method signature",
        messageFormat: "Message handler signature must be void(IMessage<TMsg>), void(Intercept, IMessage<TMsg>), void(Intercept<IMessage<TMsg>>), or IMessage?(IMessage<TMsg>)",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor MethodDoesNotSupportArray = new(
        id: "XABBO017",
        title: "Method does not support arrays",
        messageFormat: "{0} does not support arrays",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    internal static readonly DiagnosticDescriptor MethodDoesNotSupportEnumerable = new(
        id: "XABBO018",
        title: "Method does not support IEnumerable<T>",
        messageFormat: "{0} does not support IEnumerable<T>",
        category: "Xabbo",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

}
