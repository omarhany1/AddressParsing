using System;
using System.Linq;

namespace Task111
{
    public class PostCodeExtractionUtilitiesOther : IPostCodeExtractionUtilities
    {
        
        public string[] postcodeLocality(string line)
        {
            string[] x = { "", "" };
            string[] s = line.Split(' ');
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
            if (isDigitPresentBegin)
            {
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
                Console.WriteLine(postcode);
                Console.WriteLine(i);

                for (k = i; k < s.Length; k++)
                {
                    locality = locality + s[k] + ' ';
                }

                x[0] = postcode.Trim();
                x[1] = locality.Trim();
            }
            else if (isDigitPresentEnd)
            {
                int i = 0;
                int k = 0;
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
                //Console.WriteLine(postcode);
                //Console.WriteLine(i);

                for (k = i; k < s.Length; k++)
                {
                    postcode = postcode + s[k] + ' ';
                }

                x[0] = postcode.Trim();
                x[1] = locality.Trim();
            }
            else
            {
                throw new InvalidAddressException("Postcode must be supplied and it must be in beginning or end of line");
            }

            return x;
        }

    }
}