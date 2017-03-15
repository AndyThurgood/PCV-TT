using System;
using System.Net;
using System.Runtime.Remoting.Channels;

namespace PostcodeServices.FileServices
{
    /// <summary>
    /// File download service that offers methods to download a file from a specified url.
    /// a file.
    /// </summary>
    public static class FileDownloadService
    {
        public static bool DownloadRemoteFile(string targetUri, string destinationPath)
        {
            if (string.IsNullOrEmpty(targetUri))
            {
                throw new ArgumentNullException(nameof(targetUri));
            }

            if (string.IsNullOrEmpty(destinationPath))
            {
                throw new ArgumentNullException(nameof(destinationPath));
            }

            try
            {
                // Google drive doesn't seem to like html encoded uri's
                targetUri = WebUtility.HtmlDecode(targetUri);

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(targetUri, destinationPath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // This is not production code, We should always catch specific errors and react accordingly....
                Console.WriteLine(string.Join(",", "Error during file download processing: ", ex.Message));
                return false;
            }
        }
    }
}
