using System;
using System.Collections.Generic;
using Xunit;

namespace Task111.Test
{
    //Dummy Class to test functionality in CommonAddressFinder
    public class CommonTester : CommonAddressFinder
    {
        public override IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines)
        {
            throw new NotImplementedException();
        }
    }

    public class HelperMethodTests
    {
        [Fact]
        public void ShouldReturnGermanAddressFinderByCode3()
        {
           

            var sut = new AddressParsing();

            var result = sut.GetAddressFinderByCountryCode("DEU");

            Assert.NotNull(result);
            Assert.IsType(typeof(GermanIAddressFinder), result);
           
        }
        [Fact]
        public void ShouldReturnGermanAddressFinderByCode2()
        {


            var sut = new AddressParsing();

            var result = sut.GetAddressFinderByCountryCode("DE");

            Assert.NotNull(result);
            Assert.IsType(typeof(GermanIAddressFinder), result);

        }
        [Fact]
        public void ShouldReturnOtherAddressFinderByCode()
        {


            var sut = new AddressParsing();

            var result = sut.GetAddressFinderByCountryCode("EG");

            Assert.NotNull(result);
            Assert.IsType(typeof(OtherIAddressFinder), result);

        }
        [Fact]
        public void ShouldReturnGermanAddressFinderByName()
        {

            var sut = new AddressParsing();

            var result = sut.GetAddressFinderByCountryName("germany");

            Assert.NotNull(result);
            Assert.IsType(typeof(GermanIAddressFinder), result);

        }
        [Fact]
        public void ShouldReturnArrayOfSeparatedStreetHouse()
        {
            

            var sut = new CommonTester();
            //streetNo(string line)
            string line = "52 Flurstrasse";

            var result = sut.streetNo(line);

            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            Assert.Equal("52", result[0]);
            Assert.Equal("Flurstrasse", result[1]);

        }
        [Fact]
        public void ShouldReturnTheAvaialbleElementOfTheStreetAddress()
        {


            var sut = new CommonTester();
            //streetNo(string line)
            string line = "Flurstrasse";

            var result = sut.streetNo(line);

            Assert.NotNull(result);
            Assert.Equal(1, result.Length);
            Assert.Equal("Flurstrasse", result[0]);
           

        }
        [Fact]
        public void ShouldReturnSeparatedAppartmentHouse()
        {


            var sut = new GermanAddressUtilities();
            string line = "Rhondorfer Str. 666 // Appartment 47";

            var result = sut.appartmentAddressExtract(line);

            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            Assert.Equal("Appartment 47", result[1]);
            Assert.Equal("Rhondorfer Str. 666 ", result[0]);

        }
        [Fact]
        public void ShouldReturnParsedHavingHouseStreetAdded()
        {
            

            var sut = new GermanAddressUtilities();

            IDictionary<string, string> parsed = new Dictionary<string, string>();
            string[] line = {"52","flurstrasse" };

            var result = sut.addToParsedStreetHouse(parsed, line);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("flurstrasse", result["street name"]);
            Assert.Equal("52", result["house number"]);

        }
        [Fact]
        public void ShouldReturnParsedHavingHouseAdded()
        {


            var sut = new GermanAddressUtilities();

            IDictionary<string, string> parsed = new Dictionary<string, string>();
            string[] line = { "52"};

            var result = sut.addToParsedStreetHouse(parsed, line);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal("52", result["house number"]);

        }
        [Fact]
        public void ShouldReturnPostCodeLocalitySpearatedInArrayGerman()
        {


            var sut = new PostCodeExtractionUtilitiesGerman();

            string line =  "12345 Mannheim" ;

            var result = sut.postcodeLocality(line);

            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            Assert.Equal("12345", result[0]); //postcode
            Assert.Equal("Mannheim", result[1]); //locality

            string line2 = "Mannheim 12345";

            var result2 = sut.postcodeLocality(line2);

            Assert.NotNull(result2);
            Assert.Equal(2, result2.Length);
            Assert.Equal("12345", result[0]); //postcode
            Assert.Equal("Mannheim", result[1]); //locality

        }
        [Fact]
        public void ShouldReturnPostCodeLocalitySpearatedInArrayOther()
        {


            var sut = new PostCodeExtractionUtilitiesOther();

            string line = "H3Z 2Y7 MONTREAL QC";

            var result = sut.postcodeLocality(line);

            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            Assert.Equal("H3Z 2Y7", result[0]); //postcode
            Assert.Equal("MONTREAL QC", result[1]); //locality

            string line2 = "MONTREAL QC H3Z 2Y7";

            var result2 = sut.postcodeLocality(line2);

            Assert.NotNull(result2);
            Assert.Equal(2, result2.Length);
            Assert.Equal("H3Z 2Y7", result[0]); //postcode
            Assert.Equal("MONTREAL QC", result[1]); //locality
        }
        [Fact]
        public void ShouldSuccessfullyTrimInput()
        {
            var addressLines = new[]
            {
                "Herrn ",
                " Quincy Happy",
                " Wacholderweg 52a ",
                "  26133 OLDENBURG  ",
                "GERMANY  "
            };

            var sut = new StringUtilities();

            var result = sut.triminput(addressLines);

            Assert.NotNull(result);
            Assert.Equal(5, result.Length);
            Assert.Equal("Herrn", result[0]);
            Assert.Equal("Quincy Happy", result[1]);
            Assert.Equal("Wacholderweg 52a", result[2]);
            Assert.Equal("26133 OLDENBURG", result[3]);
            Assert.Equal("GERMANY", result[4]);

        }





    }
}
