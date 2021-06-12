using System;
using Magenta.Workflow.Managers.Flows;
using Magenta.Workflow.Managers.Reports;
using Microsoft.Extensions.DependencyInjection;

namespace Magenta.Workflow.Configuration
{
    public static class WorkflowConfigurationExtensions
    {
        public static WorkflowOptionsBuilder WorkflowOptionsBuilder { get; set; }
        
        public static IServiceCollection WorkflowConfigure(this IServiceCollection services,
            Action<WorkflowOptionsBuilder> options)
        {
            options(WorkflowOptionsBuilder);

            ConfigureDependencyInjections(services);

            return services;
        }

        private static void ConfigureDependencyInjections(IServiceCollection services)
        {
            //configure all di workflow needs
            services.AddScoped<IFlowManager, FlowManager>();
            services.AddScoped<IFlowReportManager, FlowReportManager>();
            services.AddScoped<IFlowManager, FlowManager>();
            
        }
    }
}
