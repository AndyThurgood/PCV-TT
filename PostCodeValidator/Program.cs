using System;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PostcodeServices.FileServices;
using PostcodeServices.Models;
using PostcodeServices.ValidationService;

namespace PostcodeValidator
{
    /// <summary>
    /// Postcode validation test harness for NHS Digital.
    /// </summary>
    public class Program
    {
        private static readonly IPostcodeValidationService ValidationService = new PostcodeValidationService();
        private static readonly string ZipfilePath = ConfigurationManager.AppSettings["ZipFileAndPath"];
        private static readonly string ImportfileUri = ConfigurationManager.AppSettings["ImportFileUri"];
        private static readonly string ImportfilePath = ConfigurationManager.AppSettings["ImportFileAndPath"];

        /// <summary>
        /// Application entry point.
        /// </summary>
        public static void Main()
        {
            int taskNumber = GetUserInput();
            GetInputFile();

            switch (taskNumber)
            {
                case 2:
                    ExecuteTaskTwo();
                    break;
                case 3:
                    ExecuteTaskThree();
                    break;
            }
        }

        /// <summary>
        /// Perform Import
        /// </summary>
        public static void ExecuteTaskTwo()
        {
            try
            {
                IList<Postcode> importedPostcodes = ImportService.ImportPostCodesFromFile(ImportfilePath, true);
                importedPostcodes = ValidationService.ValidatePostcodeCollection(importedPostcodes);
                ExportInvalidPostcodes(importedPostcodes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Join(",", "Error during processing: ", ex.Message));
            }
        }

        /// <summary>
        /// Perform import and export.
        /// </summary>
        public static void ExecuteTaskThree()
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                //Task 3, add an order by rowId to the list.
                IList<Postcode> importedPostcodes = ImportService.ImportPostCodesFromFile(ImportfilePath, true).OrderBy(x => x.Id).ToList();

                importedPostcodes = ValidationService.ValidatePostcodeCollection(importedPostcodes);

                // Performance improvement, execute list enumeration and export in parallel to leverage machine capability if available.
                Parallel.Invoke(() => ExportValidPostcodes(importedPostcodes),
                  () => ExportInvalidPostcodes(importedPostcodes));

                //ExportInvalidPostcodes(importedPostcodes);
                //ExportValidPostcodes(importedPostcodes);

                stopWatch.Stop();

                Debug.WriteLine(string.Join(" : ",
                    "Import and Export complete, time taken",
                    stopWatch.Elapsed.Seconds.ToString(),
                    stopWatch.Elapsed.TotalMilliseconds.ToString(CultureInfo.CurrentCulture)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Join(",", "Error during processing: ", ex.Message));
            }
        }

        #region Private Helper methods

        /// <summary>
        /// Checks validation status and exports invalid postcodes.
        /// </summary>
        /// <param name="postcodes">list of postcodes.</param>
        private static void ExportInvalidPostcodes(IList<Postcode> postcodes)
        {
            string failureExportFilePath = ConfigurationManager.AppSettings["ExportFailureFileAndPath"];
            ExportService.ExportPostCodeDataFile(postcodes.Where(x => !x.IsValid).ToList(),
                   failureExportFilePath, ",", "RowId, Postcode");
        }

        /// <summary>
        /// Checks validation and exports valid postcodes.
        /// </summary>
        /// <param name="postcodes"></param>
        private static void ExportValidPostcodes(IList<Postcode> postcodes)
        {
            string failureExportFilePath = ConfigurationManager.AppSettings["ExportSucessfulFileAndPath"];
            ExportService.ExportPostCodeDataFile(postcodes.Where(x => x.IsValid).ToList(),
                   failureExportFilePath, ",", "RowId, Postcode");
        }
        
        /// <summary>
        /// Prompt user to select a task to be executed.
        /// </summary>
        /// <returns>Int, the id of the task to be executed.</returns>
        private static int GetUserInput()
        {
            Console.WriteLine("Please enter task number to execute (2 / 3)");
            var taskNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(taskNumber))
            {
                GetUserInput();
            }

            int returnValue;

            if (!int.TryParse(taskNumber, out returnValue))
            {
                GetUserInput();
            }

            return returnValue;
        }

        /// <summary>
        /// Check the input file actually exists.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool DoesFileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Grabs the input file, and unzips if the file isn't present or unzipped.
        /// This is classed as util/setup functionality and not part of the overall performance peice as subsequent 
        /// execution will not require this setup.
        /// </summary>
        private static void GetInputFile()
        {
            if (!DoesFileExist(ZipfilePath) && !DoesFileExist(ImportfilePath))
            {
                Console.Write("No File found in data directory. Downloading from original source...");
                FileDownloadService.DownloadRemoteFile(ImportfileUri, ZipfilePath);
            }

            if (!DoesFileExist(ImportfilePath) && DoesFileExist(ZipfilePath))
            {
                Console.Write("Zip file provided, decompressing...");
                FileCompressionService.ExtractFile(ZipfilePath, ImportfilePath);
            }
        }

        #endregion
    }
}
