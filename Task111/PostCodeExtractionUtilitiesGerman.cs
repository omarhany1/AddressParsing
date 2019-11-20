using System;
using System.Linq;

namespace Task111
{
    public class PostCodeExtractionUtilitiesGerman : IPostCodeExtractionUtilities
    {
        public string[] postcodeLocality(string line)
        {
            string[] x = { "", "" };
            string[] s = line.Split(' ');
            if (s.Length < 2)
            {
                throw new InvalidAddressException("This line is incomplete (either missing poscode or locality or both)");
            }
            if (s[0].ToCharArray()[0] <= '9' && s[0].ToCharArray()[0] >= '0') //handling sequence of postcode and locality and multi word locality
            {
                int n;
                bool isNumeric = int.TryParse(s[0], out n);
                if ((s[0].Length != 5) || !isNumeric)
                {
                    throw new InvalidAddressException("Postcode must be exactly five digits");
                }
                //parsed.Add("postcode", s[0]);
                x[0] = s[0];
                string locality = "";
                for (int i = 1; i < s.Length; i += 1)
                {
                    if (i == s.Length - 1)
                        locality = locality + s[i];
                    else
                        locality = locality + s[i] + ' ';
                }
                //parsed.Add("locality", locality);
                x[1] = locality;

            }
            else if (s[s.Length - 1].ToCharArray()[0] <= '9' && s[s.Length - 1].ToCharArray()[0] >= '0')
            {
                int n;
                bool isNumeric = int.TryParse(s[s.Length - 1], out n);
                if ((s[s.Length - 1].Length != 5) || !isNumeric)
                {
                    throw new InvalidAddressException("Postcode must be exactly five digits");
                }
                //parsed.Add("postcode", s[s.Length - 1]);
                x[0] = s[s.Length - 1];
                string locality = "";
                for (int i = 0; i < s.Length - 1; i += 1)
                {
                    if (i == s.Length - 2)
                        locality = locality + s[i];
                    else
                        locality = locality + s[i] + ' ';
                }
                x[1] = locality;
                //parsed.Add("locality", locality);
            }
            else
            {
                throw new InvalidAddressException("Postcode must be supplied and it must be in beginning or end of line");
            }

            return x;
        }


    }
}
