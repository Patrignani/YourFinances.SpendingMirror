using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace YourFinances.SpendingMirror.Domain.Core.Interfaces
{
    public interface IConfigurationStartup
    {
        void RegisterData(IServiceCollection services, IConfiguration configuration);
        void RegisterData(IApplicationBuilder app, IConfiguration configuration);
    }
}
