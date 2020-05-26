namespace NetToolBox.SftpClient
{
    public interface ISftpClientFactory
    {
        public ISftpClient GetSftpClient(SftpSettings settings);
    }
}
