using System;
using Xabbo.Messages;
using Xunit;

namespace Xabbo.Common.Tests
{
    public class HeaderTests
    {
        [Fact(DisplayName = "Same destination, value, & name")]
        public void SameDestinationValueName()
        {
            Assert.Equal(
                new Header(Destination.Client, 100, "Named"),
                new Header(Destination.Client, 100, "Named")
            );
        }

        [Fact(DisplayName = "Same destination & value, unnamed")]
        public void SameDestinationValueUnnamed()
        {
            Assert.Equal(
                new Header(Destination.Client, 100, null),
                new Header(Destination.Client, 100, null)
            );
        }

        [Fact(DisplayName = "Different destination, same value, unnamed")]
        public void DifferentDestinationSameValueUnnamed()
        {
            Assert.NotEqual(
                new Header(Destination.Client, 100, null),
                new Header(Destination.Server, 100, null)
            );
        }

        [Fact(DisplayName = "One unknown destination, same value, unnamed")]
        public void OneUnknownDestinationSameValueUnnamed()
        {
            Assert.Equal(
                new Header(Destination.Client, 100, null),
                new Header(Destination.Unknown, 100, null)
            );
        }
    }
}
