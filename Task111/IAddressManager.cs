using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
namespace Task111
{
    /// <summary>  
    /// Based on the supplied country code/name returns  
    /// a suitable country-specific IAddressFinder implementation
    /// to extract addresses in that country. In case the supplied country
    /// is unknown/unsupported, it returns a generic IAddressFinder 
    /// implementation, which extracts a minimum set of address parts
    /// </summary>  
    /// <summary>  
    /// Based on the supplied country code/name returns  
    /// a suitable country-specific IAddressFinder implementation
    /// to extract addresses in that country. In case the supplied country
    /// is unknown/unsupported, it returns a generic IAddressFinder 
    /// implementation, which extracts a minimum set of address parts
    /// </summary>  
    public interface IAddressManager
    {

        /// <summary> 
        /// Returns an IAddressFinder implementation based on the supplied
        /// country name
        /// <remarks>
        /// Country name is multilingual (for example, both 
        /// 'Deutschland' and 'Germany' should be accepted)
        /// </remarks>
        /// </summary>
        /// 
        IAddressFinder GetAddressFinderByCountryName(string countryName);

        /// <summary> 
        /// Returns an IAddressFinder based on the supplied 2- or 3-letter    
        /// ISO country code  
        /// </summary> 
        IAddressFinder GetAddressFinderByCountryCode(string countryCode);
    }
}
