using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowInstances;

namespace Magenta.Workflow.UseCases.InitFlow
{
    public class InitFlowRequest : IFlowRequest<InitFlowModel, FlowInstance>
    {
        private FlowInstanceService InstanceService { get; }

        public InitFlowRequest(FlowInstanceService instanceService)
        {
            InstanceService = instanceService ?? throw new ArgumentNullException(nameof(instanceService));
        }

        public async Task<FlowResult<FlowInstance>> DoAsync(InitFlowModel model)
        {
            
            //TODO: take first step of instance.

            var taskResult = await InstanceService.CreateFlowInstanceAsync(model);
            return taskResult;
        }
    }
}
