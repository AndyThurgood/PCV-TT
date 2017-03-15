using System;
using System.IO;
using System.IO.Compression;

namespace PostcodeServices.FileServices
{
    /// <summary>
    /// FileCompression service that offers methods to compress or extract
    /// a file.
    /// </summary>
    public static class FileCompressionService
    {
        /// <summary>
        /// Compress a file.
        /// </summary>
        /// <param name="targetPath">The file to be compressed.</param>
        /// <param name="destinationPath">The compressed archive to be created.</param>
        /// <returns></returns>
        public static bool CompressFile(string targetPath, string destinationPath)
        {
            if (string.IsNullOrEmpty(targetPath))
            {
                throw new ArgumentNullException(nameof(targetPath));
            }

            if (string.IsNullOrEmpty(destinationPath))
            {
                throw new ArgumentNullException(nameof(destinationPath));
            }

            try
            {
                using (FileStream targetFileStream = new FileInfo(targetPath).OpenRead())
                {
                    using (FileStream destinationFilestream = File.Create(destinationPath))
                    {
                        using (GZipStream comprssionStream = new GZipStream(targetFileStream, CompressionMode.Compress))
                        {
                            destinationFilestream.CopyTo(comprssionStream);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // This is not production code, We should always catch specific errors and react accordingly....
                Console.WriteLine(string.Join(",", "Error zip processing: ", ex.Message));
                return false;
            }
        }

        /// <summary>
        /// Extract a file.
        /// </summary>
        /// <param name="targetPath">The file to be extracted.</param>
        /// <param name="destinationPath">The file to be created.</param>
        /// <returns></returns>
        public static bool ExtractFile(string targetPath, string destinationPath)
        {
            if (string.IsNullOrEmpty(targetPath))
            {
                throw new ArgumentNullException(nameof(targetPath));
            }

            if (string.IsNullOrEmpty(destinationPath))
            {
                throw new ArgumentNullException(nameof(destinationPath));
            }

            try
            {
                using (FileStream targetFileStream = new FileInfo(targetPath).OpenRead())
                {
                    using (FileStream destinationFilestream = File.Create(destinationPath))
                    {
                        using (GZipStream decomprssionStream = new GZipStream(targetFileStream, CompressionMode.Decompress))
                        {
                            decomprssionStream.CopyTo(destinationFilestream);
                        }
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                // This is not production code, We should always catch specific errors and react accordingly....
                Console.WriteLine(string.Join(",", "Error zip processing: ", ex.Message));
                return false;
            }
        }
    }
}
