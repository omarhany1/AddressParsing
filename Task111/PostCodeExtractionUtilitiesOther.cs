using System;
using System.Linq;

namespace Task111
{
    public class PostCodeExtractionUtilitiesOther : IPostCodeExtractionUtilities
    {
        /// <summary> 
        /// Class defines methods for post code and locality manipulation for German Addresses.
        /// </summary>
        



        public string[] postcodeLocality(string line)
        {
            string[] extracted = { "", "" };
            string[] s = line.Split(' ');

            //Throws exception if missing postcode or locality
            if (s.Length < 2)
            {
                throw new InvalidAddressException("This line is incomplete (either missing poscode or locality or both)");
            }

            string input1 = s[0];
            bool isDigitPresentBegin = input1.Any(c => char.IsDigit(c));

            string input2 = s[s.Length - 1];
            bool isDigitPresentEnd = input2.Any(c => char.IsDigit(c));

            string postcode = "";
            string locality = "";
            //checks if digit is present in the first word of the line
            //meaning that postcode came before locality
            if (isDigitPresentBegin)
            {
                //Loops until words have no digits meaning that we reached the locality
                //"H3Z 2Y7 MONTREAL QC" (loop till Montreal in this example)                
                int i = 0;
                int k = 0;
                for (i = 0; i < s.Length; i++)
                {
                    string current = s[i];
                    bool isDigitPresent = current.Any(c => char.IsDigit(c));
                    if (isDigitPresent)
                    {
                        postcode = postcode + current + ' ';
                    }
                    else
                    {
                        k = i;
                        break;
                    }
                }
                k -= 1;
                
                //The rest of the line is the locality
                for (k = i; k < s.Length; k++)
                {
                    locality = locality + s[k] + ' ';
                }

                extracted[0] = postcode.Trim();
                extracted[1] = locality.Trim();
            }

            //checks if digit is present in the lasy word of the line
            //meaning that locality came before postcode
            else if (isDigitPresentEnd)
            {
                int i = 0;
                int k = 0;
                //Loops until words have  digits meaning that we reached the postcode
                //"MONTREAL QC H3Z 2Y7 " (loop till H3Z in this example)
                for (i = 0; i < s.Length; i++)
                {
                    string current = s[i];
                    bool isDigitPresent = current.Any(c => char.IsDigit(c));
                    if (!isDigitPresent)
                    {
                        locality = locality + current + ' ';
                    }
                    else
                    {
                        k = i;
                        break;
                    }
                }
                k -= 1;

                //The rest of the line is the postcode
                for (k = i; k < s.Length; k++)
                {
                    postcode = postcode + s[k] + ' ';
                }

                extracted[0] = postcode.Trim();
                extracted[1] = locality.Trim();
            }
            //Throws exception if no postcode is found in beginnig or end of line
            else
            {
                throw new InvalidAddressException("Postcode must be supplied and it must be in beginning or end of line");
            }
            return extracted;
        }

    }
}