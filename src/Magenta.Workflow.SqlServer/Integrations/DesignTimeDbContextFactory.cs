using System;
using Magenta.Workflow.SqlServer.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Magenta.Workflow.SqlServer.Integrations
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorkflowDbContext>
    {
        public WorkflowDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorkflowDbContext>();
            optionsBuilder.UseSqlServer(SqlServerConfig.ConfigModel.ConnectionString);
            return new WorkflowDbContext(optionsBuilder.Options);
        }
    }
}
