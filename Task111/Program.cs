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

            AddressParsing prog = new AddressParsing();

            IAddressFinder addressFinder = null;
            /*try
            {
                addressFinder = prog.GetAddressFinderByCountryName("Deutschland");

                IEnumerable<string> address = new List<string>() { "John Doe",
                "Trippstadterstrasse",
                "67663 Kaiserslautern",
                "GERMANY" };
                IDictionary<string, string> parsed1 = addressFinder.ParseAddress(address);
                foreach (KeyValuePair<string, string> kvp in parsed1)
                {
                    //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }
            }
            catch (InvalidAddressException e)
            {
                Console.WriteLine(e.Message);
            }*/

            IAddressFinder addressFinder2 = null;
            /*try
            {
                addressFinder2 = prog.GetAddressFinderByCountryCode("EG");
                IEnumerable<string> address2 = new List<string>() { "Omar", "Flurstras 17", "   MONTREAL QC H3Z 2Y7  ", "Canada" };
                IDictionary<string, string> parsed2 = addressFinder2.ParseAddress(address2);
                foreach (KeyValuePair<string, string> kvp in parsed2)
                {
                    //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }
            }
            catch (InvalidAddressException e)
            {
                Console.WriteLine(e.Message);
            }*/

            IAddressFinder addressFinder3 = null;
            try
            {
                  
                addressFinder3 = prog.GetAddressFinderByCountryCode("DE");
                IEnumerable<string> address3 = new List<string>() { "Lara Lustig    ", "1234567", "Packstation 101", "53113 BONN", "GERMANY" };
                IDictionary<string, string> parsed3 = addressFinder3.ParseAddress(address3);
                foreach (KeyValuePair<string, string> kvp in parsed3)
                {
                    //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                }
            }
            catch (InvalidAddressException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
   
}
