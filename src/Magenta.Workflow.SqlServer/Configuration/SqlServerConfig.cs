using System;
using Magenta.Workflow.Configuration;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.SqlServer.Integrations;
using Magenta.Workflow.SqlServer.StateManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Magenta.Workflow.SqlServer.Configuration
{
    public static class SqlServerConfig
    {
        public static SqlServerConfigModel ConfigModel = new SqlServerConfigModel();

        public static WorkflowOptionsBuilder UseSqlServer(this WorkflowOptionsBuilder workflowConfiguration,
            IServiceCollection services, Action<SqlServerConfigModel> options)
        {
            options?.Invoke(ConfigModel);

            ConfigureDependencyInjection(services);

            services.AddScoped<WorkflowDbContext>();
            services.AddDbContext<WorkflowDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(ConfigModel.ConnectionString);
                if (ConfigModel.LoggerFactory != null)
                    optionsBuilder.UseLoggerFactory(ConfigModel.LoggerFactory);
                optionsBuilder.UseApplicationServiceProvider(services.BuildServiceProvider());
            });

            return workflowConfiguration;
        }

        private static void ConfigureDependencyInjection(IServiceCollection services)
        {
            //Configure mongo di
            services.AddScoped<IStateManager, SqlServerStateManager>();

        }
    }
}
