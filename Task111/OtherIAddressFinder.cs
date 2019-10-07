using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
namespace Task111
{
    public class OtherIAddressFinder : IAddressFinder
    {
        //  public void print1()
        // {
        //     Console.WriteLine("Hello World!1");
        // }
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
            string input2 = s[s.Length-1];
            bool isDigitPresentEnd = input2.Any(c => char.IsDigit(c));
            string postcode = "";
            string locality = "";
            if (isDigitPresentBegin)
            {
                int i = 0;
                int k = 0;
                for (i=0; i<s.Length; i++)
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

                for ( k=i; k<s.Length; k++)
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
                    postcode = postcode +  s[k] + ' ';
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

        public string[] streetNo(string line)
        {
            string[] x = { "", "" };
            string[] s = line.Split(' ');

            //if (s[s.Length - 1].Equals(' '))
            if (s[s.Length - 1].Length == 0)
            {
                s = s.Take(s.Count() - 1).ToArray();
            }

            if (s.Length < 2)
            {
                throw new InvalidAddressException("This line is incomplete (either missing streen name or house number or both)");
            }
            if (s[0].ToCharArray()[0] <= '9' && s[0].ToCharArray()[0] >= '0') //handling sequence of postcode and locality and multi word locality
            {
                //parsed.Add("house number", s[0]);
                x[0] = s[0];
                string streetname = "";
                for (int i = 1; i < s.Length; i += 1)
                {
                    if (i == s.Length - 1)
                        streetname = streetname + s[i];
                    else
                        streetname = streetname + s[i] + ' ';
                }
                //parsed.Add("locality", locality);
                x[1] = streetname;

            }
            else if (s[s.Length - 1].ToCharArray()[0] <= '9' && s[s.Length - 1].ToCharArray()[0] >= '0')
            {

                //parsed.Add("house number", s[s.Length - 1]);
                x[0] = s[s.Length - 1];
                //Console.WriteLine(x[0]);
                string streetname = "";
                for (int i = 0; i < s.Length - 1; i += 1)
                {
                    if (i == s.Length - 2)
                        streetname = streetname + s[i];
                    else
                        streetname = streetname + s[i] + ' ';
                }
                x[1] = streetname;
                //parsed.Add("locality", locality);
            }
            else
            {
                throw new InvalidAddressException("House number must be supplied in beginning or end of line");
            }

            return x;
        }
        public string[] triminput(string[] x)
        {
            for (int i = 0; i < x.Length; i += 1)
            {
                x[i] = x[i].Trim();
            }
            return x;
        }
        public IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines)
        {

            IDictionary<string, string> parsed = new Dictionary<string, string>();
            //x.Add("addressee",);
            string[] addressLinesArray = addressLines.ToArray();
            addressLinesArray = triminput(addressLinesArray);


            if (addressLinesArray.Length == 4)
            {
                parsed.Add("addressee", addressLinesArray[0]);

                string[] line2 = streetNo(addressLinesArray[1]);
                parsed.Add("house number", line2[0]);
                parsed.Add("street name", line2[1]);

                string[] line3 = postcodeLocality(addressLinesArray[2]);
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

                string[] line4 = postcodeLocality(addressLinesArray[3]);
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
