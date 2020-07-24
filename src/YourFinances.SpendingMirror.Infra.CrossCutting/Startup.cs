using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourFinances.SpendingMirror.Domain.Core.Interfaces;
using YourFinances.SpendingMirror.Infra.Data.Configuration;

namespace YourFinances.SpendingMirror.Infra.CrossCutting
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationStartup configClient = new ConfigurationStartup();
            configClient.RegisterData(services, configuration);

            return services;
        }

        public static IApplicationBuilder ConfigureServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            IConfigurationStartup configClient = new ConfigurationStartup();
            configClient.RegisterData(app, configuration);

            return app;
        }
    }
}
