using Microsoft.Extensions.DependencyInjection.Extensions;
using NetToolBox.SftpClient;

namespace Microsoft.Extensions.DependencyInjection
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
