using System;
using System.Collections.Generic;
using System.Linq;

namespace Task111
{
    class Program
    {
        static void Main(string[] args)
        {

            //handle bad addresees
            //handle extra spaces cases
                  


            IEnumerable<string> y = new List<string>() { "Lara Lustig", "1234567", "Packstation 101", "53113 BONN", "GERMANY"};
            //y.GetEnumerator A
            //string[] x1;
            //x1 = y.ToArray();
            //Console.WriteLine(x1[0]);
            //foreach (var item in y)
            //{
            //    Console.WriteLine(item);
            //}


            //IAddressFinder[] x = new IAddressFinder[1];
            //x[0] = new IAddressFinder1();
            GermanIAddressFinder x = new GermanIAddressFinder();
            x.ParseAddress(y);

            //foreach (KeyValuePair<string, string> kvp in parsed)
            //{
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            //}


            Console.WriteLine("Hello World!");
        }
    }

    /// <summary>  
    /// Based on the supplied country code/name returns  
    /// a suitable country-specific IAddressFinder implementation
    /// to extract addresses in that country. In case the supplied country
    /// is unknown/unsupported, it returns a generic IAddressFinder 
    /// implementation, which extracts a minimum set of address parts
    /// </summary>  
    /// <summary>  
    /// Based on the supplied country code/name returns  
    /// a suitable country-specific IAddressFinder implementation
    /// to extract addresses in that country. In case the supplied country
    /// is unknown/unsupported, it returns a generic IAddressFinder 
    /// implementation, which extracts a minimum set of address parts
    /// </summary>  
    public interface IAddressManager
    {
        //IAddressFinder addressfinder;

        //public IAddressManager(IAddressFinder addressFinder)
        //{
        //    this.addressfinder = addressfinder;
        //}
        

        /// <summary> 
        /// Returns an IAddressFinder implementation based on the supplied
        /// country name
        /// <remarks>
        /// Country name is multilingual (for example, both 
        /// 'Deutschland' and 'Germany' should be accepted)
        /// </remarks>
        /// </summary>
        /// 
        IAddressFinder GetAddressFinderByCountryName(string countryName);

        /// <summary> 
        /// Returns an IAddressFinder based on the supplied 2- or 3-letter    
        /// ISO country code  
        /// </summary> 
        IAddressFinder GetAddressFinderByCountryCode(string countryCode);
    }

    public interface IAddressFinder
    {
        /// <summary> 
        /// Extracts from the supplied multiline string a dictionary 
        /// of key value pairs representing the various parts of the 
        /// address according to standard documentation. Keys of the    
        /// IDictionary contain the names of address parts (according
        /// to UPU documentation).
        /// </summary>
        /// 
        //public void print1();
        IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines);
    }

    public class GermanIAddressFinder : IAddressFinder
    {
        public string [] postcodeLocality(string line)
        {
            string[] x = {"",""};
            string[] s = line.Split(' ');
            if (s[0].ToCharArray()[0] <= '9' && s[0].ToCharArray()[0] >= '0') //handling sequence of postcode and locality and multi word locality
            {
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
            else
            {
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

            return x;
        }
        public string[] streetNo(string line)
        {
            string[] x = { "", "" };
            string[] s = line.Split(' ');

            if(s[s.Length-1].Equals(" "))
                s = s.Take(s.Count() - 1).ToArray();

            if (s[0].ToCharArray()[0] <= '9' && s[0].ToCharArray()[0] >= '0') //handling sequence of postcode and locality and multi word locality
            {
                //parsed.Add("street number", s[0]);
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
            else
            {
                
                //parsed.Add("street number", s[s.Length - 1]);
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

            return x;
        }

        public IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines)
        {
            IDictionary<string, string> parsed = new Dictionary<string, string>();
            //x.Add("addressee",);
            string[] addressLinesArray = addressLines.ToArray();
            if (addressLinesArray.Length == 3) //Address of large volume receiver:
            {

                parsed.Add("company name", addressLinesArray[0]);

                string[] s = addressLinesArray[1].Split(' ');
                if(s[0].ToCharArray()[0] <= '9' && s[0].ToCharArray()[0] >= '0') //handling sequence of postcode and locality and multi word locality
                {
                    Console.WriteLine(s[0]);
                    parsed.Add("postcode",s[0]);
                    string locality = "";
                    for (int i = 1; i < s.Length; i += 1)
                    {
                        if(i==s.Length-1)
                            locality = locality + s[i];
                        else
                            locality = locality + s[i] + ' ';
                    }
                    Console.WriteLine(locality);
                    parsed.Add("locality", locality);

                }
                else
                {
                    Console.WriteLine(s[s.Length - 1]);
                    parsed.Add("postcode", s[s.Length-1]);
                    string locality = "";
                    for (int i = 0; i < s.Length-1; i += 1)
                    {
                        if (i == s.Length - 2)
                            locality = locality + s[i];
                        else
                            locality = locality + s[i] + ' ';
                    }
                    Console.WriteLine(locality);
                    parsed.Add("locality", locality);
                }

                //parsed.Add("postcode + locality", addressLinesArray[1]);
                parsed.Add("country", addressLinesArray[2]);
                //Console.WriteLine("enter");
                // to do add else invalid


            }
            else if (addressLinesArray.Length == 4)
            {
                parsed.Add("addressee", addressLinesArray[0]);
                if (addressLinesArray[1].Equals("Postlagernd") || addressLinesArray[1].Equals("postlagernd")) //Poste Restante address:
                {
                    parsed.Add("poste restante", addressLinesArray[1]);
                    //parsed.Add("postcode + locality", addressLinesArray[2]);
                    string[] line = postcodeLocality(addressLinesArray[2]);
                    parsed.Add("postcode", line[0]);
                    parsed.Add("locality", line[1]);
                    parsed.Add("country", addressLinesArray[3]);
                    

                }
                else if (addressLinesArray[1].Contains('/')) //Address in block of flats:
                {
                    //parsed.Add("street + No. // apartment No.", addressLinesArray[1]);
                    string streetAndNumber = "";
                    foreach (char c in addressLinesArray[1])
                    {
                        if( c== '/')
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
                    foreach (char c in addressLinesArray[1])
                    {
                        if (c == '/')
                        {
                            found = true;
                        }
                        if (found)
                        {
                            k++;
                            if(k>=4)
                                appart += c;
                        }
                    }

                    string [] line2 = streetNo(streetAndNumber);
                    parsed.Add("street number", line2[0]);
                    parsed.Add("street name", line2[1]);

                    string[] line22 = streetNo(appart);
                    parsed.Add("appartment number", line22[0]);
                    


                    string[] line3 = postcodeLocality(addressLinesArray[2]);
                    parsed.Add("postcode", line3[0]);
                    parsed.Add("locality", line3[1]);
                    parsed.Add("country", addressLinesArray[3]);
                    
                }
                else if (addressLinesArray[1].Contains("postfach") || addressLinesArray[1].Contains("Postfach"))//P.O Box address:
                {
                    string [] line2 = addressLinesArray[1].Split(' ');
                    if(line2[0].Equals("postfach") || line2[0].Equals("Postfach"))
                    {
                        string boxnumber = "";
                        for (int i = 1; i < line2.Length; i += 1)
                        {
                            boxnumber += line2[i];
                        }
                        parsed.Add("P.O Box number.", boxnumber);
                        Console.WriteLine(boxnumber);

                    }
                    else
                    {
                        string boxnumber = "";
                        for (int i = 0; i < line2.Length-1; i += 1)
                        {
                            boxnumber += line2[i];
                        }
                        parsed.Add("P.O Box number.", boxnumber);
                        Console.WriteLine(boxnumber);

                    }

                    //parsed.Add("P.O Box number.", addressLinesArray[1]);
                    string[] line = postcodeLocality(addressLinesArray[2]);
                    parsed.Add("postcode", line[0]);
                    parsed.Add("locality", line[1]);
                    parsed.Add("country", addressLinesArray[3]);

                }
                // to do add else invalid
            }
            else if (addressLinesArray.Length == 5)
            {
                char[] line2chars = addressLinesArray[1].ToCharArray();
                if (addressLinesArray[0].Equals("Herrn")) //Street address:
                {
                    parsed.Add("addressee", addressLinesArray[1]);
                    string[] line3 = streetNo(addressLinesArray[2]);
                    parsed.Add("street number", line3[0]);
                    parsed.Add("street name", line3[1]);
                    //parsed.Add("street + No.", addressLinesArray[2]);
                    string[] line4 = postcodeLocality(addressLinesArray[3]);
                    parsed.Add("postcode", line4[0]);
                    parsed.Add("locality", line4[1]);
                    //parsed.Add("postcode + locality", addressLinesArray[3]);
                    parsed.Add("country", addressLinesArray[4]);
                }
                

                else if ((line2chars[0]<='9' && line2chars[0]>='0')&&
                    (line2chars[line2chars.Length-1] <= '9' && line2chars[line2chars.Length - 1] >= '0')
                    &&(addressLinesArray[1].Split(' ').Length==1)) //“Packstation” (automatic parcel machine) addresses:
                {
                    //Console.WriteLine("enteredddd");
                    parsed.Add("addressee", addressLinesArray[0]);
                    parsed.Add("personal customer number", addressLinesArray[1]);

                    //parsed.Add("Packstation + parcel machine number", addressLinesArray[2]);
                    string[] line3 = streetNo(addressLinesArray[2]);
                    parsed.Add("parcel machine number (packstation)", line3[0]);
                    Console.WriteLine(line3[0]);

                    string[] line4 = postcodeLocality(addressLinesArray[3]);
                    parsed.Add("postcode", line4[0]);
                    parsed.Add("locality of parcel machine", line4[1]);
                    //parsed.Add("postcode + + locality of parcel machine", addressLinesArray[3]);

                    parsed.Add("country", addressLinesArray[4]);
                }
                else //Address with sub-locality:
                {
                    parsed.Add("addressee", addressLinesArray[0]);
                    parsed.Add("sub-locality", addressLinesArray[1]);
                    string[] line3 = streetNo(addressLinesArray[2]);
                    parsed.Add("street number", line3[0]);
                    parsed.Add("street name", line3[1]);
                    //parsed.Add("street + No.", addressLinesArray[2]);
                    string[] line4 = postcodeLocality(addressLinesArray[3]);
                    parsed.Add("postcode", line4[0]);
                    parsed.Add("locality", line4[1]);
                    //parsed.Add("postcode + locality", addressLinesArray[3]);
                    parsed.Add("country", addressLinesArray[4]);

                }
                // to do add else invalid
            }
            else
            {
                parsed = null;
            }


            //x.Add("txt", "notepad.exe");

            return parsed;

        }
    }

    public class IAddressFinder1 : IAddressFinder
    {
        //  public void print1()
        // {
        //     Console.WriteLine("Hello World!1");
        // }
        public IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines)
        {
            throw new NotImplementedException();
        }
    }

    


   
}
