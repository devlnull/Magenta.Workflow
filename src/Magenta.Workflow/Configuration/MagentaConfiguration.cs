using System;

namespace Magenta.Workflow.Configuration
{
    public class MagentaConfiguration
    {
        public static MagentaConfigBuilder WorkflowConfiguration(Action<MagentaConfigBuilder> options)
        {
            var configBuilder = new MagentaConfigBuilder();
            options(configBuilder);

            return configBuilder;
        }
    }
}