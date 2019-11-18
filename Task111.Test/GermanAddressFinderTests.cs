using System;
using Xunit;

namespace Task111.Test
{
    public class GermanAddressFinderTests
    {
        #region Home delivery address tests

        [Fact]
        public void ShouldSuccessfullyParseStreetAddress()
        {
            var addressLines = new[]
            {
                "Herrn",
                "Quincy Happy",
                "Wacholderweg 52a",
                "26133 OLDENBURG",
                "GERMANY"
            };

            var sut = new GermanIAddressFinder();

            var result = sut.ParseAddress(addressLines);

            Assert.NotNull(result);
            // One possible way of dealing with 
            Assert.Equal(6, result.Count);
            Assert.Equal("Quincy Happy", result["addressee"]);
            Assert.Equal("Wacholderweg", result["street name"]);
            Assert.Equal("52a", result["house number"]);
            Assert.Equal("26133", result["postcode"]);
            Assert.Equal("OLDENBURG", result["locality"]);
            Assert.Equal("GERMANY", result["country"]);

            // Another better way would've been
            // Assert.Equal(7, result.Count);
            // Assert.Equal("Herrn", result["salutation"]);
            // Assert.Equal("Quincy Happy", result["addressee"]);
            // Assert.Equal("Wacholderweg", result["street name"]);
            // Assert.Equal("52a", result["house number"]);
            // Assert.Equal("26133", result["postcode"]);
            // Assert.Equal("OLDENBURG", result["locality"]);
            // Assert.Equal("GERMANY", result["country"]);
        }

        [Fact]
        public void ShouldSuccessfullyParseAddressInBlockOfFlats()
        {
            var addressLines = new[]
            {
                "Liselotte Sommer",
                "Rhondorfer Str. 666 // Appartment 47",
                "50939 K÷LN",
                "GERMANY"
            };

            var sut = new GermanIAddressFinder();

            var result = sut.ParseAddress(addressLines);

            Assert.NotNull(result);
            Assert.Equal(7, result.Count);

        }

        [Fact]
        public void ShouldSuccessfullyParseAddressWithSubLocality()
        {
            var addressLines = new[]
            {
                "Schloﬂ Britz",
                "Ortsteil Neukoelln",
                "Alt-Britz 73",
                "12359 BERLIN",
                "GERMANY"
            };

            var sut = new GermanIAddressFinder();

            var result = sut.ParseAddress(addressLines);

            Assert.NotNull(result);
            Assert.Equal(7, result.Count);

        }


        #endregion

        #region Postal services address tests

        [Fact]
        public void ShouldSuccessfullyParsePOBoxAddress()
        {
            var addressLines = new[]
            {
                "Sitftung Warentest",
                "Postfach 3 41 41",
                "10724 BERLIN",
                "GERMANY"
            };

            var sut = new GermanIAddressFinder();

            var result = sut.ParseAddress(addressLines);

            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void ShouldSuccessfullyParseLargeVolumeReceiverAddress()
        {
            var addressLines = new[]
            {
                "Citibank Privatkunden AG",
                "68151 MANNHEIM",
                "GERMANY"
            };

            var sut = new GermanIAddressFinder();

            var result = sut.ParseAddress(addressLines);

            Assert.NotNull(result);
            Assert.Equal(4, result.Count);

        }

        [Fact]
        public void ShouldSuccessfullyParsePackstationAddress()
        {
            var addressLines = new[]
            {
                "Lara Lustig",
                "1234567",
                "Packstation 101",
                "53113 BONN",
                "GERMANY"
            };

            var sut = new GermanIAddressFinder();

            var result = sut.ParseAddress(addressLines);

            Assert.NotNull(result);
            Assert.Equal(6, result.Count);

        }


        #endregion

        #region More realistic tests


        [Fact]
        public void ShouldTryToParseLargeAddressReceiverAddressWithAdditionalDeliveryInfo()
        {
            var addressLines = new[]
            {
                "Deutsche Post AG",
                "Abteilung Briefzentren",
                "53250 BONN",
                "GERMANY"
            };

            var sut = new GermanIAddressFinder();

            var result = sut.ParseAddress(addressLines);

            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
            Assert.Equal("Deutsche Post AG", result["addressee"]);
            Assert.Equal("53250", result["postcode"]);
            Assert.Equal("BONN", result["locality"]);
            Assert.Equal("GERMANY", result["country"]);
        }

        [Fact]
        public void ShouldParseAsMuchOfTheAddressAsPossible()
        {
            var addressLines = new[]
            {
                "John Doe",
                "Trippstadterstrasse",
                "67663 Kaiserslautern",
                "GERMANY"
            };

            var sut = new GermanIAddressFinder();

            var result = sut.ParseAddress(addressLines);

            Assert.NotNull(result);
            Assert.Equal(5, result.Count);
        }


        #endregion

    }
}
