using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PostcodeServices.Models;

namespace PostcodeServices.FileServices
{
    /// <summary>
    /// Import service that imports data from raw postcode .csv files 
    /// with the format RowID, Postcode
    /// </summary>
    public static class ImportService
    {
        /// <summary>
        /// Imports .csv data into a collection of postcode objects.
        /// </summary>
        /// <param name="filePath">The file path and name of file to be imported.</param>
        /// <param name="skipFirstRow">Bool, should the first row be ignored.</param>
        /// <returns></returns>
        public static IList<Postcode> ImportPostCodesFromFile(string filePath, bool skipFirstRow)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            string[] postcodeRows = skipFirstRow 
                                    ? File.ReadAllLines(filePath).Skip(1).ToArray() 
                                    : File.ReadAllLines(filePath);

            IList<Postcode> returnList = postcodeRows.Select(postcodeRow => postcodeRow.Split(','))
                                                     .Select(values => new Postcode(values[0], values[1])).ToList();
            return returnList;
        }
    }
}
