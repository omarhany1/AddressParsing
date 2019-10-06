using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
namespace Task111
{
    public class InvalidAddressException : Exception
    {
        public InvalidAddressException()
        {

        }
        public InvalidAddressException(string exception)
            : base(String.Format(exception))
        {

        }
    }
}
