using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NETToolBox.SFTPClient
{
    public class SFtpDownloadTransfer : ISftpDownloadTransfer
    {
        private readonly ILogger<SFtpDownloadTransfer> _logger;

        public SFtpDownloadTransfer(ILogger<SFtpDownloadTransfer> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Downloads from SFTP Site and copies to the upload stream function
        /// </summary>
        /// <param name="settings">SFTP Settings (host, username,password)</param>
        /// <param name="sourceDirectory">Source directory on the SFTP site - note this does not currently support recursive downloads of subdirectories</param>
        /// <param name="uploadStream">a function that takes the filepath and stream which the transfer writes the download path and stream to</param>
        /// <param name="deleteFiles">if true, deletes the file after downloading and calling callback function</param>
        /// <param name="callBackFunction">an optional function to call with the downloaded file path after download (useful for putting an entry on a queue for subsequent processing for example</param>
        /// <returns></returns>
        public async Task TransferFilesAsync(SftpSettings settings, string sourceDirectory, Func<string, Stream, Task> uploadStream, bool deleteFiles, Func<string, Task>? callBackFunction = null)
        {
            var client = new SftpClient(settings);
            client.Connect();
            _logger.LogInformation("Connected to {Host}", settings.Host);
            var files = await client.ListDirectoryAsync(sourceDirectory).ConfigureAwait(false);
            foreach (var file in files) //just download serially, no need to get fancy with multiple download threads right now
            {
                //it's unfortunate that SSH.NET doesn't return a stream when download (instead you have to give it a stream)
                //this might be fixed in the future like https://github.com/sshnet/SSH.NET/pull/564 (although that's for SCP, not SFTP)
                //at any rate, since we aren't transferring large files and not really concerned about high throughput, we will simply allocate a memorystream to handle the transfer between the two
                using var ms = new MemoryStream();
                _logger.LogInformation("Downloading File {File}", file);
                await client.DownloadAsync(file, ms).ConfigureAwait(false);
                ms.Position = 0;
                _logger.LogInformation("Uploading File {File}", file);
                await uploadStream(file, ms).ConfigureAwait(false);
                if (callBackFunction != null)
                {
                    _logger.LogInformation("Calling CallBack Function {File}", file);
                    await callBackFunction(file).ConfigureAwait(false);
                }

                if (deleteFiles)
                {
                    _logger.LogInformation("Deleting File {File}", file);
                    client.DeleteFile(file);
                }
            }
            client.Disconnect();
        }

    }
}
