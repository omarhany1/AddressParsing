using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Task111
{
    public interface IAddressFinder
    {
        /// <summary> 
        /// Extracts from the supplied multiline string a dictionary 
        /// of key value pairs representing the various parts of the 
        /// address according to standard documentation. Keys of the    
        /// IDictionary contain the names of address parts (according
        /// to UPU documentation).
        /// </summary>


        IDictionary<string, string> ParseAddress(IEnumerable<string> addressLines);
    }
}
