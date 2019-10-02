
### Description

We would like to implement a generic address parsing library using C#. The main task of the library is to parse a multiline string containing a postal address and identify its individual parts (e.g. addressee, street, house no, ... and so on). The input addresses have varying country-specific formats. To get a better idea of how such formats vary, take a look at the following 2 sample links to see documentation for [Canadian](http://www.upu.int/fileadmin/documentsFiles/activities/addressingUnit/canEn.pdf) as well as [German](http://www.upu.int/fileadmin/documentsFiles/activities/addressingUnit/deuEn.pdf)  addresses

The following C# interfaces are meant to help you start structuring your code:  
  
```  
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
    IAddressFinder GetAddressFinderByCountryName(string countryName);  
 
    /// <summary> 
    /// Returns an IAddressFinder based on the supplied 2- or 3-letter    
    /// ISO country code  
    /// </summary> 
    IAddressFinder GetAddressFinderByCountryCode(string countryCode);
}  
```  
  
and
  
```  
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
```  
  
For the purpose of this assessment, your library implementation should support extracting German addresses according to [standard documentation](http://www.upu.int/fileadmin/documentsFiles/activities/addressingUnit/deuEn.pdf) from [UPU](http://www.upu.int). In case, the supplied address, wasn't identified as a German one, please provide a fallback implementation, which extracts the following address parts:  
- a mandatory addressee  
- optional additional delivery information  
- mandatory street address
- mandatory house no.  
- mandatory city  
- mandatory postal code
- mandatory country name

Following is the generic template using `<placeholders>` for a multiline address, whose country is unidentified/unsupported:
```
<addressee>  
[<additional delivery info>]   
<street and house no.>  
<city and postal code>  
<country>
```
  
Please take as many as you can of the following concepts into consideration while implementing:  
- Polymorphism  
- Extensibility  
- Inversion of control and support for dependency injection
- Exception handling
- Support for logging

### Deliverable
Expected is a private github repo containing a Visual Studio solution, composed of:  
1. your implementation (possibly broken into more than one project),  
2. a test project.
  
You could use the address examples included in the UPU documentation as valid input samples. Your first commit to the repo should be this markdown file.

### Delivery time
After three days of starting work on your assignment, I would like to see the state of your implementation. So, please be transparent about your start time. The goal of this assessment is not to submit a complete, perfect implementation. It is to assess:
- how fit you are in coding using .NET  
- your understanding and application of software design concepts, like the ones mentioned above   

   
