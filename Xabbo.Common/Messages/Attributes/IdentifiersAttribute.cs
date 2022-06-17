using System;
using System.Collections.Generic;
using System.Linq;

using Xabbo.Common;

namespace Xabbo.Messages.Attributes
{
    public abstract class IdentifiersAttribute : Attribute
    {
        /// <summary>
        /// Specifies if this attribute is required for the interceptor binding to succeed.
        /// </summary>
        public bool Required { get; set; } = true;
        /// <summary>
        /// Specifies the clients which are required for the interceptor binding to succeed.
        /// </summary>
        public ClientType RequiredClient { get; set; } = ClientType.Flash | ClientType.Unity;
        /// <summary>
        /// Specifies the message identifiers.
        /// </summary>
        public IReadOnlyList<Identifier> Identifiers { get; }

        public IdentifiersAttribute(Destination destination, params string[] identifiers)
        {
            if (identifiers is null || identifiers.Length == 0)
                throw new ArgumentException("At least one identifier must be defined.", nameof(identifiers));

            Identifiers = identifiers
                .Select(name => new Identifier(destination, name))
                .Distinct()
                .ToList()
                .AsReadOnly();
        }
    }
}
