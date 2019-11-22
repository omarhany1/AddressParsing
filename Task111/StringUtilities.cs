using System;
using System.Collections.Generic;

namespace Task111
{
    public class StringUtilities : IStringUtilities
    {

        /// <summary>
        /// Performs the trimming of all lines in a given addresse.
        /// </summary>
        /// /// <param name="x">The input lines of the address after being coverted into array of lines.</param>
        /// /// <returns>
        /// The trimmed version.
        /// </returns>

        public string[] triminput(string[] x)
        {
            for (int i = 0; i < x.Length; i += 1)
            {
                x[i] = x[i].Trim();
            }
            return x;
        }

       
    }
}
