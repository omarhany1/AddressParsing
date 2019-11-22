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

        /// <summary>
        /// The constructor of the Other Address Finder that instatiates
        /// and an address finder for non German Addresses.
        /// Dependecies of that class are injected in the constructor.
        /// They are needed for string manipulation and extracting information.
        /// </summary>
        public OtherIAddressFinder(IStringUtilities stringUtilities, IPostCodeExtractionUtilities postCodeExtraction)
        {
            this.stringUtilities = stringUtilities;
            this.postCodeExtraction = postCodeExtraction;
        }

        /// <summary>
        /// The main address parsing method for non German Addresses.
        /// Contains the logic behind identifying the relevant information in
        /// a given address and parses it.
        /// </summary>
        /// <param name="addressLines">The input lines of the address as initially received.</param>
        /// /// <returns>
        /// The parsed address in the form of an IDictionary
        /// where each pair is the extracted valus from the address and its corresponding label.
        /// </returns>
        
        public override IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines)
        {

            IDictionary<string, string> parsed = new Dictionary<string, string>();
            string[] addressLinesArray = addressLines.ToArray();


            addressLinesArray = stringUtilities.triminput(addressLinesArray);

            //Non German Address with no additional info
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

            //Non German Address with additional info
            else if (addressLinesArray.Length == 5)
            {
                parsed.Add("addressee", addressLinesArray[0]);
                parsed.Add("additional delivery info", addressLinesArray[1]);

                string[] line3 = streetNo(addressLinesArray[2]);
                parsed.Add("house number", line3[0]);
                parsed.Add("street name", line3[1]);

                string[] line4 = postCodeExtraction.postcodeLocality(addressLinesArray[3]);
                parsed.Add("postcode", line4[0]);
                parsed.Add("locality", line4[1]);

                parsed.Add("country", addressLinesArray[4]);
            }

            //Throw exception if non German address of such unknown format is entered.
            else
            {
                throw new InvalidAddressException("Non german addresses must consist of either 4 or 5 lines only");
            }
            return parsed;
        }

        
    }
}
