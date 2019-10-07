using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
namespace Task111
{
    public class AddressParsing : IAddressManager
    {


        public static readonly string[] germanyNames = { "Deutschland", "Germany","Duitsland","Gjermania","لمانيا","Германія","Германия","Alemanya","Německo","Tyskland","Duitsland",
                "Saksamaa","آلمان","Saksa","Allemagne", "Alemaña","Γερμανία","גרמניה","जर्मनी","Németország",
                "Þýskalanfd", "Jerman", "An Ghearmáin", "Germania","ドイツ", "독일", "Vācija", "Vokietija", "Германија",
                "德国", "Tyskland", "Niemcy", "Alemanha", "Germania", "Германия", "Немачка", "Nemecko", "Nemčija",
                "Alemania", "Ujerumani", "Tyskland", "Alemanya", "ประเทศเยอรมัน", "Almanya", "Німеччина", "Đức", "Yr Almaen"};

        public IAddressFinder GetAddressFinderByCountryCode(string countryCode)
        {
            if (!(countryCode.Length == 2 || countryCode.Length == 3))
            {
                throw new InvalidAddressException("invalid iso");
            }
            countryCode = countryCode.ToLowerInvariant();
            if (countryCode.Equals("de") || countryCode.Equals("deu"))
            {
                return new GermanIAddressFinder();
            }
            else
            {
                return new OtherIAddressFinder();
            }
        }
        ///public abstract IAddressFinder GetAddressFinderByCountryName(string countryName);
        public IAddressFinder GetAddressFinderByCountryName(string countryName)
        {
          countryName = countryName.ToLowerInvariant();
         foreach (string s in germanyNames)
        {
          if (s.ToLowerInvariant().Equals(countryName))
             return new GermanIAddressFinder();
         }
        return new OtherIAddressFinder();
        }
    }
}
