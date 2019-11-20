using System;
using Xunit;

namespace Task111.Test
{
    public class OtherAddressFinderTests
    {
        [Fact]
        public void ShouldSuccessfullyParseStreetAddressWithNoAtTheEnd()
        {
            var addressLines = new[]
            {
                "John Doe",
                "Baker street 23",
                "26133 London",
                "England"
            };

            var sut = new OtherIAddressFinder(new StringUtilities(), new PostCodeExtractionUtilitiesOther());

            var result = sut.ParseAddress(addressLines);

            Assert.NotNull(result);
            Assert.Equal(6, result.Count);
            Assert.Equal("John Doe", result["addressee"]);
            Assert.Equal("23", result["house number"]);
            Assert.Equal("Baker street", result["street name"]);
            Assert.Equal("26133", result["postcode"]);
            Assert.Equal("London", result["locality"]);
            Assert.Equal("England", result["country"]);
        }

        [Fact]
        public void ShouldSuccessfullyParseStreetAddressWithNoAtTheBeginning()
        {
            var addressLines = new[]
            {
                "John Doe",
                "23 Baker street",
                "26133 London",
                "England"
            };

            var sut = new OtherIAddressFinder(new StringUtilities(), new PostCodeExtractionUtilitiesOther());

            var result = sut.ParseAddress(addressLines);

            Assert.NotNull(result);
            Assert.Equal(6, result.Count);
            Assert.Equal("John Doe", result["addressee"]);
            Assert.Equal("23", result["house number"]);
            Assert.Equal("Baker street", result["street name"]);
            Assert.Equal("26133", result["postcode"]);
            Assert.Equal("London", result["locality"]);
            Assert.Equal("England", result["country"]);
        }
    }
}
