using System;
using System.Collections.Generic;

namespace Task111
{
    public class GermanAddressUtilities : IGermanAddressUtilities
    {
        /// <summary>
        /// For the address in block of flats format,
        /// it separates the line in the form  "Rhondorfer Str. 666 // Appartment 47",
        /// to "Rhondorfer Str. 666"  & "Appartment 47"
        /// </summary>
        /// /// <param name="line">The line with the format above.</param>
        /// /// <returns>
        /// Sperated into two strings and returned in to a 2 element string array.
        /// </returns>
        public string[] appartmentAddressExtract(string line)
        {
            string[] separatedAppartmentAddress = { "", "" };

            string streetAndNumber = "";
            //Loop adds the first part of the line before the "//"
            foreach (char c in line)
            {
                if (c == '/')
                {
                    break;
                }
                else
                {
                    streetAndNumber += c;
                }
            }


            string appart = "";
            bool found = false;
            int k = 0;
            //Loop adds the second part of the line after the "//"
            foreach (char c in line)
            {
                if (c == '/')
                {
                    found = true;
                }
                if (found)
                {
                    k++;
                    if (k >= 4)
                        appart += c;
                }
            }
            separatedAppartmentAddress[0] = streetAndNumber;
            separatedAppartmentAddress[1] = appart;
            
            return separatedAppartmentAddress;
        }

        /// <summary>
        /// Adds as much of information as it could find about the
        /// street name and house number to the parsed template.
        /// </summary>
        /// /// <param name="parsed">The information parsed from the address so far.</param>
        /// /// <param name="line">The line containg information of street name and house number.</param>
        /// /// <returns>
        /// The parsed template added to it the info it could retrieve from the supplied line.
        /// </returns>
        public IDictionary<string, string> addToParsedStreetHouse(IDictionary<string, string> parsed, string[] line)
        {
            //checks if both house number and street name are present
            if (line.Length == 2)
            {
                parsed.Add("house number", line[0]);
                parsed.Add("street name", line[1]);
            }
            else
            {
                //if only one is present checks if it is the street or house by finding if it starts
                //with digit
                if (line[0].ToCharArray()[0] <= '9' && line[0].ToCharArray()[0] >= '0')
                {
                    parsed.Add("house number", line[0]);
                }
                else
                {
                    parsed.Add("street name", line[0]);

                }
            }
            return parsed;
        }
    }
}
