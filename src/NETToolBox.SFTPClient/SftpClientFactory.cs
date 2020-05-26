namespace NetToolBox.SftpClient
{
    public sealed class SftpClientFactory : ISftpClientFactory
    {
        public ISftpClient GetSftpClient(SftpSettings settings)
        {
            return new SftpClient(settings);
        }
    }
}
