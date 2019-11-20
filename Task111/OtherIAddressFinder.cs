using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
namespace Task111
{
    public class OtherIAddressFinder : CommonAddressFinder
    {
        IStringUtilities stringUtilities;
        IPostCodeExtractionUtilities postCodeExtraction;

        public OtherIAddressFinder(IStringUtilities stringUtilities, IPostCodeExtractionUtilities postCodeExtraction)
        {
            this.stringUtilities = stringUtilities;
            this.postCodeExtraction = postCodeExtraction;
        }

       

        
       
        public override IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines)
        {

            IDictionary<string, string> parsed = new Dictionary<string, string>();
            string[] addressLinesArray = addressLines.ToArray();
            addressLinesArray = stringUtilities.triminput(addressLinesArray);


            if (addressLinesArray.Length == 4)
            {
                parsed.Add("addressee", addressLinesArray[0]);

                string[] line2 = streetNo(addressLinesArray[1]);
                parsed.Add("house number", line2[0]);
                parsed.Add("street name", line2[1]);

                string[] line3 = postCodeExtraction.postcodeLocality(addressLinesArray[2]);
                parsed.Add("postcode", line3[0]);
                parsed.Add("locality", line3[1]);

                parsed.Add("country", addressLinesArray[3]);
            }
            else if (addressLinesArray.Length == 5)
            {
                parsed.Add("addressee", addressLinesArray[0]);
                parsed.Add("additional delivery info", addressLinesArray[1]); // to dooo

                string[] line3 = streetNo(addressLinesArray[2]);
                parsed.Add("house number", line3[0]);
                parsed.Add("street name", line3[1]);

                string[] line4 = postCodeExtraction.postcodeLocality(addressLinesArray[3]);
                parsed.Add("postcode", line4[0]);
                parsed.Add("locality", line4[1]);

                parsed.Add("country", addressLinesArray[4]);
            }
            else
            {
                throw new InvalidAddressException("Non german addresses must cinsist of either 4 or 5 lines only");
            }



            return parsed;
        }

        
    }
}
