using System;
using Magenta.Workflow.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Magenta.Workflow.Redis.Configuration
{
    public static class RedisConfig
    {
        public static RedisConfigModel ConfigModel = new RedisConfigModel();

        public static WorkflowOptionsBuilder UseRedis(this WorkflowOptionsBuilder workflowConfiguration,
            IServiceCollection services, Action<RedisConfigModel> options)
        {
            options?.Invoke(ConfigModel);

            ConfigureDependencyInjection(services);

            return workflowConfiguration;
        }

        private static void ConfigureDependencyInjection(IServiceCollection services)
        {
            //Configure redis di
        }
    }
}
