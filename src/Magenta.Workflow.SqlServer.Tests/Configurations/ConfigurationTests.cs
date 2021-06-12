using Magenta.Workflow.Configuration;
using Magenta.Workflow.Redis.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Magenta.Workflow.Redis.Tests.Configurations
{
    public class ConfigurationTests
    {
        [Fact]
        public void ConfigureRedis_WithCorrectAction_MustSetRedisConfigs()
        {
            //Arrange
            var services = new ServiceCollection();
            string connectionString = "testConnectionString";
            string instanceName = "testInstanceName";
            //Act
            services.WorkflowConfigure(options =>
            {
                options.UseRedis(services, redisOptions =>
                {
                    redisOptions.ConnectionString = connectionString;
                    redisOptions.InstanceName = instanceName;
                });
            });
            //Assert
            Assert.Equal(connectionString, RedisConfig.ConfigModel.ConnectionString);
            Assert.Equal(instanceName, RedisConfig.ConfigModel.InstanceName);
        }
    }
}
