using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourFinances.SpendingMirror.Infra.CrossCutting;

namespace YourFinances.SpendingMirror.Application.API.Midlleware
{
    public static class ConfigurationStartup
    {
        public static IServiceCollection AddApplication(this IServiceCollection service, IConfiguration configuration)
        {
            service.ConfigureServices(configuration);

            return service;
        }

        public static IApplicationBuilder AddApplication(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.ConfigureServices(configuration);

            return app;
        }
    }
}
