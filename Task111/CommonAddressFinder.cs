using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
namespace Task111
{
    /// <summary>
    /// This class contains methods that will be used
    /// in both German and non German Address Finders.
    /// </summary>
    
    public abstract class CommonAddressFinder : IAddressFinder
    {

        /// <summary>
        /// Responsible for extracting the streen name and house number in the line
        /// that contains this information.
        /// </summary>
        /// <param name="line">The input line that contains the street name and house number.</param>
        /// /// <returns>
        /// The extracted relevant street and number relevant information
        /// where each represents an element in the array.
        /// </returns>

        public string[] streetNo(string line)
        {
            string[] extracted = { "", "" };
            string[] splittedLine = line.Split(' ');

            //Checks for extra spaces and removes them
            if (splittedLine[splittedLine.Length - 1].Length == 0)
            {
                splittedLine = splittedLine.Take(splittedLine.Count() - 1).ToArray();
            }

            //Checks if the line is missing values for streen name or house number.
            if (splittedLine.Length < 2)
            {
                //If the line is missing one of them it just returns
                //the relevant found information (either the street name or the house number          
                if (splittedLine.Length == 1)
                {
                    string availableString = splittedLine[0];
                    
                    extracted = new string[1] ;
                    extracted[0] = availableString;
                    return extracted;
                }
                else
                    throw new InvalidAddressException("This line is empty");
            }
            else
            {
                //Checks if the house number is in the beginning of the line
                if (splittedLine[0].ToCharArray()[0] <= '9' && splittedLine[0].ToCharArray()[0] >= '0')
                {
                    //Puts the house number in the first position of the extracted info array
                    extracted[0] = splittedLine[0];


                    string streetname = "";
                    for (int i = 1; i < splittedLine.Length; i += 1)
                    {
                        if (i == splittedLine.Length - 1)
                            streetname = streetname + splittedLine[i];
                        else
                            streetname = streetname + splittedLine[i] + ' ';
                    }
                    extracted[1] = streetname;

                }

                //Checks if the house number is in the end of the line
                else if (splittedLine[splittedLine.Length - 1].ToCharArray()[0] <= '9' && splittedLine[splittedLine.Length - 1].ToCharArray()[0] >= '0')
                {

                    extracted[0] = splittedLine[splittedLine.Length - 1];
                    string streetname = "";
                    for (int i = 0; i < splittedLine.Length - 1; i += 1)
                    {
                        if (i == splittedLine.Length - 2)
                            streetname = streetname + splittedLine[i];
                        else
                            streetname = streetname + splittedLine[i] + ' ';
                    }
                    extracted[1] = streetname;
                }
                //This is the case were there is only a street name and it is multi word
                else
                {
                    string streetname = "";
                    for (int i = 0; i < splittedLine.Length - 1; i += 1)
                    {
                        if (i == splittedLine.Length - 2)
                            streetname = streetname + splittedLine[i];
                        else
                            streetname = streetname + splittedLine[i] + ' ';
                    }
                    extracted = new string[1];
                    extracted[0] = streetname;
                    
                }
            }
            return extracted;
        }


        public abstract IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines);
        
    }
}
