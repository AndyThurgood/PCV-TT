using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostcodeServices.FileServices;
using PostcodeServices.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ValidationTests.Integration
{   
    [TestClass]
    public class PostcodeValidatorIntegrationTests
    {
        /// <summary>
        /// Feels a bit strange to leverage code that is part of the integration test to build the test context, just roll with
        /// it, as the requirement is to not bundle any files and we are testing for integration of the end 2 end here.
        /// We are also not testing the commandline element of the program nor the validation and file download (Not enough time).
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            IList<Postcode> postcodes = new List<Postcode>
            {
                new Postcode(1, "gjkfgfdjk"),
                new Postcode(200, "B32 1EE"),
                new Postcode(4, "TF9 9DU"),
                new Postcode(9, "N22 5QT"),
                new Postcode(10, "BS20 0BW"),
                new Postcode(12, "SW17 0DQ"),
                new Postcode(5, "LS2 7AD"),
                new Postcode(100, "HX6 1NQ"),
                new Postcode(60, "gjkfgfdjk"),
                new Postcode(3, "gjkfgfdjk"),
                new Postcode(6, "gjkfgfdjk"),
                new Postcode(11, "gjkfgfdjk"),
                new Postcode(1000, "gjkfgfdjk")
            };

            ExportService.ExportPostCodeDataFile(postcodes, "IntegrationTest.csv", ",", "RowId, Postcode");
        }

        [TestMethod]
        public void TaskTwo_ShouldGenerate_FailedOutputFile()
        {
            PostcodeValidator.Program.ExecuteTaskTwo();
            Assert.IsTrue(File.Exists("failed_validation.csv"));
            IList<Postcode> postcodes = ImportService.ImportPostCodesFromFile("failed_validation.csv", true);
            Assert.IsTrue(postcodes.Count == 6);
        }

        [TestMethod]
        public void TaskThree_ShouldGenerate_FailedOutputFile()
        {
            PostcodeValidator.Program.ExecuteTaskThree();
            Assert.IsTrue(File.Exists("failed_validation.csv"));
            IList<Postcode> postcodes = ImportService.ImportPostCodesFromFile("failed_validation.csv", true);
            Assert.IsTrue(postcodes.Count == 6);
        }

        [TestMethod]
        public void TaskThree_ShouldGenerate_ValidOutputFile()
        {
            PostcodeValidator.Program.ExecuteTaskThree();
            Assert.IsTrue(File.Exists("failed_validation.csv"));
            IList<Postcode> postcodes = ImportService.ImportPostCodesFromFile("succeeded_validation.csv", true);
            Assert.IsTrue(postcodes.Count == 7);
            // crud check to see if the collection is ordered.
            Assert.IsTrue(postcodes.FirstOrDefault().Id < postcodes.LastOrDefault().Id);
        }
    }
}
