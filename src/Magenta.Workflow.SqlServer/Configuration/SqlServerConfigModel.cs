using System;
using Microsoft.Extensions.Logging;

namespace Magenta.Workflow.SqlServer.Configuration
{
    public class SqlServerConfigModel
    {
        public string ConnectionString { get; set; }
        public ILoggerFactory LoggerFactory { get; set; }
    }
}
