using System;
using System.Text;

namespace Xabbo.Messages.Dispatcher;

public sealed class UnknownIdentifiersException : Exception
{
    public Identifiers Identifiers { get; }

    public UnknownIdentifiersException(Identifiers identifiers)
        : base(BuildMessage(identifiers))
    {
        Identifiers = identifiers;
    }

    private static string BuildMessage(Identifiers identifiers)
    {
        var sb = new StringBuilder();

        sb.Append("Unknown identifiers. ");
        sb.Append(identifiers.ToString());

        return sb.ToString();
    }
}
