using Magenta.Workflow.Configuration;

namespace Magenta.Workflow.SqlServer.Configuration
{
    public static class SqlServerConfig
    {
        public static MagentaConfigBuilder UseSqlServer(this MagentaConfigBuilder configBuilder, 
            string connectionString)
        {
            SqlServerConfigModel.ConnectionString = connectionString;

            return configBuilder;
        }
    }
}
