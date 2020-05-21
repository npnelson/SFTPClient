using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace NetToolBox.SftpClient
{
    public static class SftpClientServiceCollectionExtensions
    {
        public static IServiceCollection AddSftpClient(this IServiceCollection services)
        {
            services.TryAddSingleton<ISftpDownloadTransfer, SFtpDownloadTransfer>();
            return services;
        }
    }
}
