using System;
using System.Linq;

namespace Task111
{
    /// <summary> 
    /// Class defines methods for post code and locality manipulation for German Addresses.
    /// </summary>
    
    public class PostCodeExtractionUtilitiesGerman : IPostCodeExtractionUtilities
    {
        
        public string[] postcodeLocality(string line)
        {
            string[] extracted = { "", "" };
            string[] s = line.Split(' ');

            //Throws exception if missing postcode or locality
            if (s.Length < 2)
            {
                throw new InvalidAddressException("This line is incomplete (either missing poscode or locality or both)");
            }

            //checks if the first character is digit (so first word is post code)
            if (s[0].ToCharArray()[0] <= '9' && s[0].ToCharArray()[0] >= '0') 
            {
                int n;
                bool isNumeric = int.TryParse(s[0], out n);
                //Throws exception if postcode is not all numeric and doesn't consist of 5 digits
                if ((s[0].Length != 5) || !isNumeric)
                {
                    throw new InvalidAddressException("Postcode must be exactly five digits");
                }
                //Puts the postcode in the first position of the extracted info array
                extracted[0] = s[0];

                string locality = "";
                for (int i = 1; i < s.Length; i += 1)
                {
                    if (i == s.Length - 1)
                        locality = locality + s[i];
                    else
                        locality = locality + s[i] + ' ';
                }
                extracted[1] = locality;

            }
            //checks if the last character is digit (so last word is post code)
            else if (s[s.Length - 1].ToCharArray()[0] <= '9' && s[s.Length - 1].ToCharArray()[0] >= '0')
            {
                int n;
                bool isNumeric = int.TryParse(s[s.Length - 1], out n);

                //Throws exception if postcode is not all numeric and doesn't consist of 5 digits
                if ((s[s.Length - 1].Length != 5) || !isNumeric)
                {
                    throw new InvalidAddressException("Postcode must be exactly five digits");
                }
                //Puts the postcode in the last position of the extracted info array
                extracted[0] = s[s.Length - 1];

                string locality = "";
                for (int i = 0; i < s.Length - 1; i += 1)
                {
                    if (i == s.Length - 2)
                        locality = locality + s[i];
                    else
                        locality = locality + s[i] + ' ';
                }
                extracted[1] = locality;
                
            }
            //Throws exception if no post code is found in beginning or end of line
            else
            {
                throw new InvalidAddressException("Postcode must be supplied and it must be in beginning or end of line");
            }

            return extracted;
        }


    }
}
