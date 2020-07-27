using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleOAuth;
using SimpleOAuth.Models;
using YourFinances.SpendingMirror.Domain.Core.DTOs;
using YourFinances.SpendingMirror.Infra.CrossCutting;

namespace YourFinances.SpendingMirror.Application.API.Midlleware
{
    public static class ConfigurationStartup
    {
        public static IServiceCollection AddApplication(this IServiceCollection service, IConfiguration configuration)
        {
            service
                .ConfigureServices(configuration)
                .AddSimpleOAuth(option =>
                {
                       option.AddKeyToken(configuration.GetValue<string>("AuthConfiguration:Token"));
                })
                .AddScoped(serviceProvider =>
                {
                    var token = serviceProvider.GetRequiredService<TokenRead>();
                    var value = new SessionUser();

                    if (token != null && token.Claims != null)
                    {
                        var user_Id = token.GetValue("Id_User");
                        var account_Id = token.GetValue("Account_Id");

                        if (int.TryParse(user_Id, out int userId) && int.TryParse(account_Id, out int accountId))
                        {
                            value.Id = userId;
                            value.AccountId = accountId;
                        }
                    }

                    return value;
                });

            return service;
        }

        public static IApplicationBuilder AddApplication(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.ConfigureServices(configuration);

            return app;
        }
    }
}
