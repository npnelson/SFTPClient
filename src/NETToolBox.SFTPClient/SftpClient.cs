﻿using Renci.SshNet.Async;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetToolBox.SftpClient
{
    public sealed class SftpClient : ISftpClient
    {
        private readonly Renci.SshNet.SftpClient _internalClient;
        public SftpClient(string host, string userName, string password)
        {

            _internalClient = new Renci.SshNet.SftpClient(host, userName, password);


        }

        public string Host { get { return _internalClient.ConnectionInfo.Host; } }

        public void Connect()
        {
            _internalClient.Connect();
        }

        public void DeleteFile(string path)
        {
            _internalClient.Delete(path);
        }

        public void Disconnect()
        {
            _internalClient.Disconnect();
        }

        public Task DownloadAsync(string path, Stream downloadStream)
        {
            return _internalClient.DownloadAsync(path, downloadStream);
        }

        public async Task<List<string>> ListDirectoryAsync(string path)
        {
            var files = await _internalClient.ListDirectoryAsync(path).ConfigureAwait(false);
            return files.Select(x => x.FullName.Substring(1)).Where(x => !x.EndsWith(".")).ToList(); //we don't want to return . or .. and we don't want to return first / either
        }

        public Task UploadAsync(string path, Stream uploadStream)
        {
            return _internalClient.UploadAsync(uploadStream, path);
        }
    }
}
