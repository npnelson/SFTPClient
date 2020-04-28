using System;
using System.IO;
using System.Threading.Tasks;

namespace NETToolBox.SFTPClient
{
    public interface ISftpDownloadTransfer
    {
        Task TransferFilesAsync(SftpSettings settings, string sourceDirectory, Func<string, Stream, Task> uploadStream, bool deleteFiles, Func<string, Task>? callBackFunction = null);
    }
}
