using System;
using System.Collections.Generic;
using System.Linq;

namespace Xabbo.Messages
{
    public abstract class IdentifiersAttribute : Attribute
    {
        public IReadOnlyList<Identifier> Identifiers { get; }

        public IdentifiersAttribute(Destination destination, params string[] identifiers)
        {
            if (identifiers is null || identifiers.Length == 0)
                throw new ArgumentException("At least one identifier must be defined", nameof(identifiers));

            Identifiers = identifiers
                .Select(name => new Identifier(destination, name))
                .Distinct()
                .ToList()
                .AsReadOnly();
        }
    }
}
