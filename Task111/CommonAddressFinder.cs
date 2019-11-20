using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
namespace Task111
{
    public abstract class CommonAddressFinder : IAddressFinder
    {
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
                
                if (s.Length == 1)
                {
                    string availableString = "";
                    for (int i = 0; i < s.Length; i += 1)
                    {
                        if (i == s.Length - 1)
                            availableString = availableString + s[i];
                        else
                            availableString = availableString + s[i] + ' ';
                    }
                    string[] y = {"" };
                    y[0] = availableString;
                    return y;
                }
                else
                    throw new InvalidAddressException("This line is incomplete (either missing streen name or house number or both)");
            }
            else
            {
                if (s[0].ToCharArray()[0] <= '9' && s[0].ToCharArray()[0] >= '0')
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
            }
            return x;
        }


        public abstract IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines);
        
    }
}
