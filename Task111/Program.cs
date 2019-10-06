using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;


namespace Task111
{
    class Program
    {
        static void Main(string[] args)
        {

            //handle bad addresees
            //handle extra spaces cases

            //int n;
            //bool isNumeric = int.TryParse("78202", out n);
            //Console.WriteLine(isNumeric);
            //string input = "helloworld";
            //bool isDigitPresent = input.Any(c => char.IsDigit(c));
            //Environment.Exit(-1);
            AddressParsing prog = new AddressParsing();
            IAddressFinder addressFinder = null;
            try
            {
                addressFinder = prog.GetAddressFinderByCountryCode("EG");
                IEnumerable<string> y = new List<string>() { "Omar", "Flurstras 17", "   MONTREAL QC H3Z 2Y7  ", "Germany" };
                IDictionary<string, string> parsed = addressFinder.ParseAddress(y);
                foreach (KeyValuePair<string, string> kvp in parsed)
                {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                  Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }
            }
            catch (InvalidAddressException e)
            {
                Console.WriteLine(e.Message);
            }
            
                

                Console.WriteLine("Hello World!");
        }
    }
   
}
