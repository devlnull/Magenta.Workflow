using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowInstances;

namespace Magenta.Workflow.UseCases.InitFlow
{
    public class InitFlowRequest : IFlowRequest<InitFlowModel, FlowInstance>
    {
        private readonly FlowInstanceService _instanceService;

        public InitFlowRequest(FlowInstanceService instanceService)
        {
            _instanceService = instanceService ?? throw new ArgumentNullException(nameof(instanceService));
        }

        public async Task<FlowResult<FlowInstance>> DoAsync(InitFlowModel model)
        {
            
            //TODO: take first step of instance.

            var taskResult = await _instanceService.CreateFlowInstanceAsync(model);
            return taskResult;
        }
    }
}
