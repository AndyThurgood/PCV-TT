using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostcodeServices.FileServices;
using PostcodeServices.Models;

namespace ValidationTests.Unit
{
    [TestClass]
    public class PostcodeFileServicesTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "filePath")]
        public void FileExportService_ShouldThrow_WhenFilePathEmpty()
        {
            IList<Postcode> validPostCodes = new List<Postcode>
            {
                new Postcode(1,"EC1A 1BB"),
                new Postcode(2,"W1A 0AX"),
                new Postcode(3,"M1 1AE"),
                new Postcode(4,"B33 8TH"),
                new Postcode(5,"CR2 6XH"),
                new Postcode(6,"DN55 1PT"),
                new Postcode(7,"GIR 0AA"),
                new Postcode(8,"SO10 9AA"),
                new Postcode(9, "FY9 9AA"),
                new Postcode(10,"WC1A 9AA")
            };

            ExportService.ExportPostCodeDataFile(validPostCodes, "", ",", "RowId, Postcode");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "postcodes")]
        public void FileExportService_ShouldThrow_WhenPostcodesNull()
        {
            ExportService.ExportPostCodeDataFile(null, "Path", ",", "RowId, Postcode");
        }

        [TestMethod]
        public void FileExportService_ShouldGenerate_PostcodeFile()
        {
            IList<Postcode> validPostCodes = new List<Postcode>
            {
                new Postcode(1,"EC1A 1BB"),
                new Postcode(2,"W1A 0AX"),
                new Postcode(3,"M1 1AE"),
                new Postcode(4,"B33 8TH"),
                new Postcode(5,"CR2 6XH"),
                new Postcode(6,"DN55 1PT"),
                new Postcode(7,"GIR 0AA"),
                new Postcode(8,"SO10 9AA"),
                new Postcode(9, "FY9 9AA"),
                new Postcode(10,"WC1A 9AA")
            };

            ExportService.ExportPostCodeDataFile(validPostCodes, "Import_data.csv", ",", "RowId, Postcode");
            Assert.IsTrue(File.Exists("Import_data.csv"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "filePath")]
        public void FileImportService_ShouldThrow_WhenFilePathEmpty()
        {
            ImportService.ImportPostCodesFromFile("", true);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "File not found")]
        public void FileImportService_ShouldThrow_WhenFileDoesNotExist()
        {
            ImportService.ImportPostCodesFromFile("Import_data_missing.csv", true);
        }

        [TestMethod]
        public void FileExportService_ShouldRead_PostcodeFile()
        {
            IList<Postcode> validPostCodes = new List<Postcode>
            {
                new Postcode(1,"EC1A 1BB"),
                new Postcode(2,"W1A 0AX"),
                new Postcode(3,"M1 1AE"),
                new Postcode(4,"B33 8TH"),
                new Postcode(5,"CR2 6XH"),
                new Postcode(6,"DN55 1PT"),
                new Postcode(7,"GIR 0AA"),
                new Postcode(8,"SO10 9AA"),
                new Postcode(9, "FY9 9AA"),
                new Postcode(10,"WC1A 9AA")
            };

            ExportService.ExportPostCodeDataFile(validPostCodes, "Import_data.csv", ",", "RowId, Postcode");
            var importList = ImportService.ImportPostCodesFromFile("Import_data.csv", true);
            Assert.AreEqual(validPostCodes.Count, importList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "filePath")]
        public void FileCompressionService_ShouldThrow_WhenCompress_FilePathEmpty()
        {
            FileCompressionService.CompressFile("", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "destinationPath")]
        public void FileCompressionService_ShouldThrow_WhenCompress_DestintationPathEmpty()
        {
            FileCompressionService.CompressFile("filePath", "");
        }

        [TestMethod]
        public void FileCompressionService_ShouldCompresseFile()
        {
            FileCompressionService.CompressFile("Import_data.csv", "Import_data.gz");
            Assert.IsTrue(File.Exists("Import_data.gz"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "filePath")]
        public void FileCompressionService_ShouldThrow_WhenExtract_FilePathEmpty()
        {
            FileCompressionService.ExtractFile("", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "destinationPath")]
        public void FileCompressionService_ShouldThrow_WhenExtract_DestintationPathEmpty()
        {
            FileCompressionService.ExtractFile("filePath", "");
        }

        [TestMethod]
        public void FileCompressionService_ShouldExtractFile()
        {
            FileCompressionService.ExtractFile("Import_data.gz", "Import_data_extract.csv");
            Assert.IsTrue(File.Exists("Import_data_extract.csv"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "filePath")]
        public void FileDownloadService_ShouldThrow_WhenDownload_FilePathEmpty()
        {
            FileDownloadService.DownloadRemoteFile("", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "destinationPath")]
        public void FileDownloadService_ShouldThrow_WhenDownload_DestintationPathEmpty()
        {
            FileDownloadService.DownloadRemoteFile("www.google.com", "");
        }
    }
}
