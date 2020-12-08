using Magenta.Workflow.Configuration;
using Magenta.Workflow.SqlServer.Configuration;
using Xunit;

namespace Magenta.Workflow.Tests.Configurations
{
    public class SqlServerConfigureTests
    {
        [Fact]
        public void Configure_SqlServerConfig_CanSetConnectionString()
        {
            var result = MagentaConfiguration.WorkflowConfiguration(options =>
            {
                options.UseInMemory();
                options.UseSqlServer("test");
            });
            //Assert
            Assert.True(result.InMemoryState);
        }
    }
}
