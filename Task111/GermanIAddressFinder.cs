using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Task111
{
    public class GermanIAddressFinder : CommonAddressFinder
    {
        IStringUtilities stringUtilities;
        IPostCodeExtractionUtilities postCodeExtraction;
        IGermanAddressUtilities germanAddressUtilities;

        /// <summary>
        /// The constructor of the German Address Finder that instatiates
        /// and an address finder for German Addresses.
        /// Dependecies of that class are injected in the constructor.
        /// They are needed for string manipulation and extracting information.
        /// </summary>

        public GermanIAddressFinder(IStringUtilities stringUtilities, IPostCodeExtractionUtilities postCodeExtraction
            ,IGermanAddressUtilities germanAddressUtilities)
        {
            this.stringUtilities = stringUtilities;
            this.postCodeExtraction = postCodeExtraction;
            this.germanAddressUtilities = germanAddressUtilities;
        }

        /// <summary>
        /// Performs the parsing of German addresses that contain three lines only.
        /// </summary>
        /// /// <param name="addressLinesArray">The input lines of the address after being coverted into array of lines.</param>
        /// /// <returns>
        /// The three lines German address parsed to its correct template.
        /// </returns>
        
        public IDictionary<string, string> threeLinesGermanIdentifier(string[] addressLinesArray)
        {
            IDictionary<string, string> parsed = new Dictionary<string, string>();

            parsed.Add("company name", addressLinesArray[0]);

            string[] line2 = postCodeExtraction.postcodeLocality(addressLinesArray[1]);
            parsed.Add("postcode", line2[0]);
            parsed.Add("locality", line2[1]);

            //Throws exception if the country is not Germany
            if (!(addressLinesArray[2].ToLowerInvariant().Equals("germany")))
                throw new InvalidAddressException("Third line must be country and German address must have Germany as a country");

            parsed.Add("country", "GERMANY");

            return parsed;
        }

        /// <summary>
        /// Performs the parsing of German addresses that contain four lines.
        /// </summary>
        /// /// <param name="addressLinesArray">The input lines of the address after being coverted into array of lines.</param>
        /// /// <returns>
        /// The four lines German address parsed to its correct template.
        /// </returns>
        public IDictionary<string, string> fourLinesGermanIdentifier(string[] addressLinesArray)
        {
            IDictionary<string, string> parsed = new Dictionary<string, string>();

            //Common for all 4 lines German addresses
            parsed.Add("addressee", addressLinesArray[0]);

            //Throws exception if fourth line is not germany as it should be
            if (!(addressLinesArray[3].ToLowerInvariant().Equals("germany")))
                throw new InvalidAddressException("Fourth line must be country and German address must have Germany as a country");

            parsed.Add("country", "GERMANY");

            //Poste Restante address identifier:
            if (addressLinesArray[1].ToLowerInvariant().Equals("postlagernd"))
            {
                parsed.Add("poste restante", "Postlagernd");

                string[] line = postCodeExtraction.postcodeLocality(addressLinesArray[2]);
                parsed.Add("postcode", line[0]);
                parsed.Add("locality", line[1]);


            }

            //Address in block of flats identifier:
            else if (addressLinesArray[1].Contains("//"))
            {
                //(addressLinesArray[1] is in format "Rhondorfer Str. 666 // Appartment 47",
                //this line separates it to "Rhondorfer Str. 666"  & "Appartment 47"
                string[] separatedAppartmentAddress = germanAddressUtilities.appartmentAddressExtract(addressLinesArray[1]);


                string[] line21 = streetNo(separatedAppartmentAddress[0]);

                //add the relevant information about the street address
                parsed = germanAddressUtilities.addToParsedStreetHouse(parsed, line21);

                //Uses the street and house separator
                //to separate appartment and number
                string[] line22 = streetNo(separatedAppartmentAddress[1]);

                parsed.Add("appartment number", line22[0]);

                string[] line3 = postCodeExtraction.postcodeLocality(addressLinesArray[2]);
                parsed.Add("postcode", line3[0]);
                parsed.Add("locality", line3[1]);
            }

            // P.O Box address identifier:
            else if (addressLinesArray[1].ToLowerInvariant().Contains("postfach"))
            {
                string[] line2 = addressLinesArray[1].Split(' ');

                //Checks if the first word in the line was postfach
                if (line2[0].ToLowerInvariant().Equals("postfach"))
                {
                    //Box number is contained in the rest of the line
                    string boxnumber = "";
                    for (int i = 1; i < line2.Length; i += 1)
                    {
                        if (i == 1)
                        {
                            if (!(line2[i].Length == 1 || line2[i].Length == 2))
                                throw new InvalidAddressException("Incorrect PO box number format");
                        }
                        else
                        {
                            if (!(line2[i].Length == 2))
                                throw new InvalidAddressException("Incorrect PO box number format");
                        }
                        boxnumber += line2[i];
                    }
                    parsed.Add("P.O Box number.", boxnumber);

                }

                //Checks if the last word in the line was postfach
                else if (line2[line2.Length - 1].ToLowerInvariant().Equals("postfach"))
                {
                    //Box number is contained in the beginning of the line
                    string boxnumber = "";
                    for (int i = 0; i < line2.Length - 1; i += 1)
                    {
                        if (i == 0)
                        {
                            if (!(line2[i].Length == 1 || line2[i].Length == 2))
                                throw new InvalidAddressException("Incorrect PO box number format");
                        }
                        else
                        {
                            if (!(line2.Length == 2))
                                throw new InvalidAddressException("Incorrect PO box number format");
                        }
                        boxnumber += line2[i];
                    }
                    parsed.Add("P.O Box number.", boxnumber);

                }

                //Throws exception if the word postfach is mixed between the numbers
                else
                {
                    throw new InvalidAddressException("postfach must be either at end or start of line 2 (not between the numbers)");
                }

                string[] line = postCodeExtraction.postcodeLocality(addressLinesArray[2]);
                parsed.Add("postcode", line[0]);
                parsed.Add("locality", line[1]);

            }
            //Street address: v2
            //It is the case if the 4 lines address is not any of the abvove cases
            else
            {
                string[] line2 = streetNo(addressLinesArray[1]);

                //add the relevant information about the street address
                parsed = germanAddressUtilities.addToParsedStreetHouse(parsed, line2);

                string[] line = postCodeExtraction.postcodeLocality(addressLinesArray[2]);
                parsed.Add("postcode", line[0]);
                parsed.Add("locality", line[1]);


            }

            return parsed;
        }

        /// <summary>
        /// Performs the parsing of German addresses that contain five lines.
        /// </summary>
        /// /// <param name="addressLinesArray">The input lines of the address after being coverted into array of lines.</param>
        /// /// <returns>
        /// The five lines German address parsed to its correct template.
        /// </returns>
        public IDictionary<string, string> fiveLinesGermanIdentifier(string[] addressLinesArray)
        {
            IDictionary<string, string> parsed = new Dictionary<string, string>();

            //Line4 of all Five Line German Addresses contains Postcode + Locality
            string[] line4 = postCodeExtraction.postcodeLocality(addressLinesArray[3]);
            parsed.Add("postcode", line4[0]);
            parsed.Add("locality", line4[1]);

            //Line5 of all Five Line German Addresses contains Country
            //Throws exception if fourth line is not germany as it should be
            if (!(addressLinesArray[4].ToLowerInvariant().Equals("germany")))
                throw new InvalidAddressException("Fifth line must be country and German address must have Germany as a country");

            
            parsed.Add("country", "GERMANY");


            //Checks if line 2 is all numeric
            int n;
            bool isLine2Numeric = int.TryParse(addressLinesArray[1], out n);

            //Line 2 being all numeric is the identifier of the “Packstation” (automatic parcel machine) addresses:
            if (isLine2Numeric) 
            {
                parsed.Add("addressee", addressLinesArray[0]);

                //Throws exception if customer number doesn't have correct number of digits
                if (!(addressLinesArray[1].Length >= 6 && addressLinesArray[1].Length <= 10))
                    throw new InvalidAddressException("Personal customer number must be 6-10 digits");

                parsed.Add("personal customer number", addressLinesArray[1]);

                //Separates packstation word and its digit using the streen No separator
                //Line is in format "Packstation 101"
                string[] line3 = streetNo(addressLinesArray[2]);

                //Checks that the number is all numeric (not in form 12b)
                int k;
                bool isPackStNumberNumeric = int.TryParse(line3[0], out k);

                //Throws exception if packtation number is not all numeric
                if (!isPackStNumberNumeric)
                    throw new InvalidAddressException("Packstation number must contain numbers only");

                parsed.Add("parcel machine number (packstation)", line3[0]);
  
            }

            //Street address: v1 identifier
            else if (addressLinesArray[0].Equals("Herrn")) 
            {
                parsed.Add("addressee", addressLinesArray[1]);
                string[] line3 = streetNo(addressLinesArray[2]);

                //add the relevant information about the street address
                parsed = germanAddressUtilities.addToParsedStreetHouse(parsed,line3);
                
  
            }
            //The remaining cases indicates that it is Address with sub-locality format:
            else
            {
                parsed.Add("addressee", addressLinesArray[0]);
                parsed.Add("sub-locality", addressLinesArray[1]);
                string[] line3 = streetNo(addressLinesArray[2]);

                //add the relevant information about the street address
                parsed = germanAddressUtilities.addToParsedStreetHouse(parsed, line3);

            }

            return parsed;
        }

        
        /// <summary>
        /// The main address parsing method for German Addresses.
        /// Contains the logic behind identifying the relevant information in
        /// a given address and parses it.
        /// </summary>
        /// <param name="addressLines">The input lines of the address as initially received.</param>
        /// /// <returns>
        /// The parsed address in the form of an IDictionary
        /// where each pair is the extracted valus from the address and its corresponding label.
        /// </returns>
        /// 
        public override IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines)
        {
            IDictionary<string, string> parsed = new Dictionary<string, string>();

            string[] addressLinesArray = addressLines.ToArray();
            addressLinesArray = stringUtilities.triminput(addressLinesArray);


            //Address of large volume receiver:
            //Identified by having 3 lines only
            if (addressLinesArray.Length == 3) 
            {
                parsed = threeLinesGermanIdentifier(addressLinesArray);     

            }

            // Other German addresses that are identified by
            // having 4 lines
            else if (addressLinesArray.Length == 4)
            { 
                parsed = fourLinesGermanIdentifier(addressLinesArray);
            }
            else if (addressLinesArray.Length == 5)
            {
                parsed = fiveLinesGermanIdentifier(addressLinesArray);             
            }

            //Throws exception if the German address is not in the expected number of lines
            else
            {
                throw new InvalidAddressException("German address must be 3, 4 or 5 lines only");
            }

            return parsed;

        }


        
    }

}
