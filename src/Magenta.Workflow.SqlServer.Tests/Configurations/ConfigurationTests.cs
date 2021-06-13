using Magenta.Workflow.Configuration;
using Magenta.Workflow.SqlServer.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Magenta.Workflow.SqlServer.Tests.Configurations
{
    public class ConfigurationTests
    {
        [Fact]
        public void ConfigureSqlServer_WithCorrectAction_MustSetSqlServerConfigs()
        {
            //Arrange
            var services = new ServiceCollection();
            string connectionString = "testConnectionString";
            //Act
            services.WorkflowConfigure(options =>
            {
                options.UseSqlServer(services, sqlServerOptions =>
                {
                    sqlServerOptions.ConnectionString = connectionString;
                });
            });
            //Assert
            Assert.Equal(connectionString, SqlServerConfig.ConfigModel.ConnectionString);
        }
    }
}
