using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NETToolBox.SFTPClient
{
    public interface ISftpClient
    {
        void Connect();
        void Disconnect();
        void DeleteFile(string path);
        Task<List<string>> ListDirectoryAsync(string path);
        Task DownloadAsync(string path, Stream downloadStream);
        Task UploadAsync(string path, Stream uploadStream);

    }
}
