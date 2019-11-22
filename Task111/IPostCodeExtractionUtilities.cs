using System;
namespace Task111
{
    public interface IPostCodeExtractionUtilities
    {
        /// <summary> 
        /// Class defines useful methods
        /// for dealing with post code and locality.
        /// Used by bith German and non German address finders.
        /// </summary>
        ///

        /// <summary>
        /// Responsible for extracting the post code and locality from
        /// a given line that holds this information.
        /// </summary>
        /// <param name="line">The input line that contains the postcode + locality.</param>
        /// /// <returns>
        /// The extracted relevant postcode and locality relevant information
        /// where each represents an element in the string array.
        /// </returns>
        public string[] postcodeLocality(string line);
    }
}
