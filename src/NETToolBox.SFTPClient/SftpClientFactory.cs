namespace NetToolBox.SftpClient
{
    public sealed class SftpClientFactory : ISftpClientFactory
    {
        public ISftpClient GetSftpClient(string host, string userName, string password)
        {
            return new SftpClient(host, userName, password);
        }
    }
}
