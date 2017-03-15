using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PostcodeServices.Models;

namespace PostcodeServices.FileServices
{
    /// <summary>
    /// Export service that exports data to a raw .csv file 
    /// with the format RowID, Postcode
    /// </summary>
    public static class ExportService
    {
        /// <summary>
        /// Export postcode data to .csv file.
        /// </summary>
        /// <param name="postcodes">A list of postcodes to be exported.</param>
        /// <param name="filePath">The file path and name of file to be exported.</param>
        /// <param name="seperator">The seperator to signify as a new field in the csv export.</param>
        /// <param name="header">An optional header to include as the first csv row.</param>
        public static void ExportPostCodeDataFile(IList<Postcode> postcodes, string filePath, string seperator, string header = null)
        {
            if (postcodes == null)
            {
                throw new ArgumentNullException(nameof(postcodes));
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            List<string> outputList = new List<string>();

            if (header != null)
            {
                outputList.Add(header);
            }

            outputList.AddRange(postcodes.Select(x => string.Join(seperator, x.Id, x.Value)));

            File.WriteAllLines(filePath, outputList);
        }
    }
}
