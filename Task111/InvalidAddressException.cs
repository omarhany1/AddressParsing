using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
namespace Task111
{
    public class InvalidAddressException : Exception
    {
        /// <summary> 
        /// The class that defines the exception that
        /// is thrown if an address is invalid.
        /// </summary>
        
        public InvalidAddressException()
        {

        }
        public InvalidAddressException(string exception)
            : base(String.Format(exception))
        {

        }
    }
}
