namespace NetToolBox.SftpClient
{
    public interface ISftpClientFactory
    {
        public ISftpClient GetSftpClient(string host, string userName, string password);
    }
}
