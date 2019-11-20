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

        public GermanIAddressFinder(IStringUtilities stringUtilities, IPostCodeExtractionUtilities postCodeExtraction)
        {
            this.stringUtilities = stringUtilities;
            this.postCodeExtraction = postCodeExtraction;
        }

        public override IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines)
        {
            IDictionary<string, string> parsed = new Dictionary<string, string>();
            //x.Add("addressee",);
            string[] addressLinesArray = addressLines.ToArray();
            addressLinesArray = stringUtilities.triminput(addressLinesArray);



            if (addressLinesArray.Length == 3) //Address of large volume receiver:
            {

                parsed.Add("company name", addressLinesArray[0]);

                string[] line2 = postCodeExtraction.postcodeLocality(addressLinesArray[1]);
                parsed.Add("postcode", line2[0]);
                parsed.Add("locality", line2[1]);

                if (!(addressLinesArray[2].ToLowerInvariant().Equals("germany")))
                    throw new InvalidAddressException("Third line must be country and German address must have Germany as a country");
                parsed.Add("country", addressLinesArray[2]);
                


            }
            else if (addressLinesArray.Length == 4)
            {
                parsed.Add("addressee", addressLinesArray[0]);
                if (addressLinesArray[1].Equals("Postlagernd") || addressLinesArray[1].Equals("postlagernd")) //Poste Restante address:
                {
                    parsed.Add("poste restante", addressLinesArray[1]);
                    //parsed.Add("postcode + locality", addressLinesArray[2]);
                    string[] line = postCodeExtraction.postcodeLocality(addressLinesArray[2]);
                    parsed.Add("postcode", line[0]);
                    parsed.Add("locality", line[1]);

                    if (!(addressLinesArray[3].ToLowerInvariant().Equals("germany")))
                        throw new InvalidAddressException("Fourth line must be country and German address must have Germany as a country");

                    parsed.Add("country", addressLinesArray[3]);


                }
                else if (addressLinesArray[1].Contains("//")) //Address in block of flats:
                {

                    //parsed.Add("street + No. // apartment No.", addressLinesArray[1]);
                    string streetAndNumber = "";
                    foreach (char c in addressLinesArray[1])
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
                    foreach (char c in addressLinesArray[1])
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
                    //Console.WriteLine(streetAndNumber);
                    string[] line2 = streetNo(streetAndNumber);

                    if (line2.Length == 2)
                    {
                        parsed.Add("house number", line2[0]);
                        parsed.Add("street name", line2[1]);
                    }
                    else
                    {
                        if (line2[0].ToCharArray()[0] <= '9' && line2[0].ToCharArray()[0] >= '0')
                        {
                            parsed.Add("house number", line2[0]);
                        }
                        else
                        {
                            parsed.Add("street name", line2[0]);

                        }
                    }

                    string[] line22 = streetNo(appart);

                    parsed.Add("appartment number", line22[0]);



                    string[] line3 = postCodeExtraction.postcodeLocality(addressLinesArray[2]);
                    parsed.Add("postcode", line3[0]);
                    parsed.Add("locality", line3[1]);

                    if (!(addressLinesArray[3].ToLowerInvariant().Equals("germany")))
                        throw new InvalidAddressException("Fourth line must be country and German address must have Germany as a country");

                    parsed.Add("country", addressLinesArray[3]);

                }
                else if (addressLinesArray[1].Contains("postfach") || addressLinesArray[1].Contains("Postfach"))//P.O Box address:
                {
                    string[] line2 = addressLinesArray[1].Split(' ');
                    if (line2[0].Equals("postfach") || line2[0].Equals("Postfach"))
                    {
                        string boxnumber = "";
                        for (int i = 1; i < line2.Length; i += 1)
                        {
                            if(i == 1)
                            {
                                if (!(line2[i].Length == 1 || line2[i].Length==2))
                                    throw new InvalidAddressException("Incorrect PO box number format");
                            }
                            else {
                                if (!(line2[i].Length==2))
                                    throw new InvalidAddressException("Incorrect PO box number format");
                            }
                            boxnumber += line2[i];
                        }
                        parsed.Add("P.O Box number.", boxnumber);
                        Console.WriteLine(boxnumber);

                    }
                    else if (line2[line2.Length - 1].Equals("postfach") || line2[line2.Length - 1].Equals("Postfach"))
                    {
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
                        Console.WriteLine(boxnumber);

                    }
                    else
                    {
                        throw new InvalidAddressException("postfach must be either at end or start of line 2 (not between the numbers)");
                    }

                    //parsed.Add("P.O Box number.", addressLinesArray[1]);
                    string[] line = postCodeExtraction.postcodeLocality(addressLinesArray[2]);
                    parsed.Add("postcode", line[0]);
                    parsed.Add("locality", line[1]);
                    parsed.Add("country", addressLinesArray[3]);

                }
                else //Street address: v2
                {
                    //parsed.Add("addresse", addressLinesArray[0]);
                    string[] line2 = streetNo(addressLinesArray[1]);
                    if (line2.Length == 2)
                    {
                        parsed.Add("house number", line2[0]);
                        parsed.Add("street name", line2[1]);
                    }
                    else
                    {
                        
                        if (line2[0].ToCharArray()[0] <= '9' && line2[0].ToCharArray()[0] >= '0')
                        {
                            parsed.Add("house number", line2[0]);
                        }
                        else
                        {
                            parsed.Add("street name", line2[0]);

                        }
                    }

                    string[] line = postCodeExtraction.postcodeLocality(addressLinesArray[2]);
                    parsed.Add("postcode", line[0]);
                    parsed.Add("locality", line[1]);

                    if (!(addressLinesArray[3].ToLowerInvariant().Equals("germany")))
                        throw new InvalidAddressException("Fourth line must be country and German address must have Germany as a country");

                    parsed.Add("country", addressLinesArray[3]);
                }
                // to do add else invalid
            }
            else if (addressLinesArray.Length == 5)
            {
                int n;
                bool isLine2Numeric = int.TryParse(addressLinesArray[1], out n);

                char[] line2chars = addressLinesArray[1].ToCharArray();

                if (addressLinesArray[0].Equals("Herrn")) //Street address: v1
                {
                    parsed.Add("addressee", addressLinesArray[1]);
                    string[] line3 = streetNo(addressLinesArray[2]);
                    if (line3.Length == 2)
                    {
                        parsed.Add("house number", line3[0]);
                        parsed.Add("street name", line3[1]);
                    }
                    else
                    {
                        if (line3[0].ToCharArray()[0] <= '9' && line3[0].ToCharArray()[0] >= '0')
                        {
                            parsed.Add("house number", line3[0]);
                        }
                        else
                        {
                            parsed.Add("street name", line3[0]);

                        }
                    }
                    //parsed.Add("street + No.", addressLinesArray[2]);
                    string[] line4 = postCodeExtraction.postcodeLocality(addressLinesArray[3]);
                    parsed.Add("postcode", line4[0]);
                    parsed.Add("locality", line4[1]);
                    //parsed.Add("postcode + locality", addressLinesArray[3]);

                    if (!(addressLinesArray[4].ToLowerInvariant().Equals("germany")))
                        throw new InvalidAddressException("Fifth line must be country and German address must have Germany as a country");


                    parsed.Add("country", addressLinesArray[4]);
                }


                else if (isLine2Numeric) //“Packstation” (automatic parcel machine) addresses:
                {
                    //Console.WriteLine("enteredddd");
                    parsed.Add("addressee", addressLinesArray[0]);

                    if (!(addressLinesArray[1].Length >= 6 && addressLinesArray[1].Length <= 10))
                        throw new InvalidAddressException("Personal customer number must be 6-10 digits");

                    parsed.Add("personal customer number", addressLinesArray[1]);

                    
                    //parsed.Add("Packstation + parcel machine number", addressLinesArray[2]);
                    string[] line3 = streetNo(addressLinesArray[2]);

                    int k;
                    bool isPackStNumberNumeric = int.TryParse(line3[0], out k);

                    if (!isPackStNumberNumeric)
                        throw new InvalidAddressException("Packstation number must contain numbers only");

                    parsed.Add("parcel machine number (packstation)", line3[0]);
                    Console.WriteLine(line3[0]);

                    string[] line4 = postCodeExtraction.postcodeLocality(addressLinesArray[3]);
                    parsed.Add("postcode", line4[0]);
                    parsed.Add("locality of parcel machine", line4[1]);
                    //parsed.Add("postcode + + locality of parcel machine", addressLinesArray[3]);

                    if (!(addressLinesArray[4].ToLowerInvariant().Equals("germany")))
                        throw new InvalidAddressException("Fifth line must be country and German address must have Germany as a country");

                    parsed.Add("country", addressLinesArray[4]);
                }
                else //Address with sub-locality:
                {
                    parsed.Add("addressee", addressLinesArray[0]);
                    parsed.Add("sub-locality", addressLinesArray[1]);
                    string[] line3 = streetNo(addressLinesArray[2]);
                    if (line3.Length == 2)
                    {
                        parsed.Add("house number", line3[0]);
                        parsed.Add("street name", line3[1]);
                    }
                    else
                    {
                        if (line3[0].ToCharArray()[0] <= '9' && line3[0].ToCharArray()[0] >= '0')
                        {
                            parsed.Add("house number", line3[0]);
                        }
                        else
                        {
                            parsed.Add("street name", line3[0]);

                        }
                    }
                    //parsed.Add("street + No.", addressLinesArray[2]);
                    string[] line4 = postCodeExtraction.postcodeLocality(addressLinesArray[3]);
                    parsed.Add("postcode", line4[0]);
                    parsed.Add("locality", line4[1]);
                    //parsed.Add("postcode + locality", addressLinesArray[3]);

                    if (!(addressLinesArray[4].ToLowerInvariant().Equals("germany")))
                        throw new InvalidAddressException("Fifth line must be country and German address must have Germany as a country");

                    parsed.Add("country", addressLinesArray[4]);

                }
                // to do add else invalid
            }
            else
            {
                throw new InvalidAddressException("German address must be 3, 4 or 5 lines only");
            }


            //x.Add("txt", "notepad.exe");

            return parsed;

        }

        
    }

}
