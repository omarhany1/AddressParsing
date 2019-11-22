using System;
using System.Collections.Generic;

namespace Task111
{
    public interface IGermanAddressUtilities
    {
        /// <summary> 
        /// Contains the needed helper methods for German address parsing.
        /// </summary>
        
        public string[] appartmentAddressExtract(string line);
        public IDictionary<string, string> addToParsedStreetHouse(IDictionary<string, string> parsed, string[] line);
    }
}
