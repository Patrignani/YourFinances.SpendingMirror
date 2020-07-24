using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using YourFinances.SpendingMirror.Domain.Core.Interfaces;

namespace YourFinances.SpendingMirror.Infra.Data.Configuration
{
    public class ConfigurationStartup : IConfigurationStartup
    {

        public void RegisterData(IServiceCollection services, IConfiguration configuration)
        {
            ServiceData(services, configuration);
        }

        public void RegisterData(IApplicationBuilder app, IConfiguration configuration)
        {
            ConfigurationDatabase(app);
        }

        private void ServiceData(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new NpgsqlConnectionStringBuilder(configuration.GetConnectionString("YourFinacesServices")));
        }

        #region Banco de Dados
        public void ConfigurationDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var connectionStringBuilder = scope.ServiceProvider.GetRequiredService<NpgsqlConnectionStringBuilder>();
                RegisterDabase(connectionStringBuilder);
                using (var connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString))
                {
                    GetVersion(connection, out var version);
                    ExecuteVersion(version, connection);
                }
            }
        }

        private void RegisterDabase(NpgsqlConnectionStringBuilder connectionStringBuilder)
        {
            var databaseName = connectionStringBuilder.Database;
            connectionStringBuilder.Database = "";

            using (var connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString))
            {
                var result = connection.ExecuteScalar<int>(@"SELECT COUNT(datname) FROM pg_database WHERE datname =@DatabaseName", new { DatabaseName = databaseName });

                if (result == 0)
                {
                    connection.Execute($@"CREATE DATABASE ""{databaseName}""  WITH
                                              OWNER = postgres
                                              ENCODING = 'UTF8'
                                              CONNECTION LIMIT = -1");

                    connectionStringBuilder.Database = databaseName;
                    CreateVersionTable(connectionStringBuilder);
                }
                connectionStringBuilder.Database = databaseName;
            }
        }
        private void CreateVersionTable(NpgsqlConnectionStringBuilder connectionStringBuilder)
        {
            using (var connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString))
            {
                var script = @"CREATE TABLE ""Version""(
                                    ""Id"" SERIAL PRIMARY KEY,
                                    ""Version"" VARCHAR(50) NOT NULL,
                                    ""Application"" VARCHAR(50) NOT NULL,
                                    ""DateUpdate"" TIMESTAMP)";
                connection.Execute(script);
            }
        }
        private void GetVersion(NpgsqlConnection connection, out int version)
        {
            version = connection.ExecuteScalar<int>(@"SELECT ""Version"" FROM PUBLIC.""Version"" 
                                                            WHERE ""Application""= @Application
                                                            ORDER BY ""DateUpdate"" DESC FETCH FIRST 1 ROWS ONLY",
                            new { Application = "YourFinances.SpendingMirror.Application" });
        }

        private void UpdateVersion(int version, NpgsqlConnection connection)
        {
            var script = @"INSERT INTO public.""Version""(""Version"", ""Application"", ""DateUpdate"")
                           VALUES(@Version, @Application, @DateUpdate);";

            connection.Execute(script, new
            {
                Version = version,
                Application = "YourFinances.SpendingMirror.Application",
                DateUpdate = DateTime.Now
            });
        }
        #region versions
        private void ExecuteVersion(int version, NpgsqlConnection connection)
        {
            switch (version)
            {
                case 0:
                    Version1(ref version, connection);
                    UpdateVersion(version, connection);
                    ExecuteVersion(version, connection);
                    break;
            }
        }

        private void Version1(ref int version, NpgsqlConnection connection)
        {
            var script = @"CREATE TABLE ""SpendingMirror""(
                                    ""Id"" SERIAL PRIMARY KEY,
                                    ""CashFlowGrouping"" VARCHAR (250) NOT NULL,
                                    ""Value"" Decimal(10,2) NOT NULL,
                                    ""DateRegister"" TIMESTAMP,
                                    ""Observation"" VARCHAR (250) NOT NULL,
                                    ""AccountId"" numeric NOT NULL
                            )";

            connection.Execute(script);

            version = 1;
        }
        #endregion

        #endregion
    }
}

