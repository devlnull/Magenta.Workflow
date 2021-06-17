using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowStates;

namespace Magenta.Workflow.UseCases.InitFlowState
{
    public class InitFlowStateRequestHandler : IFlowRequestHandler<InitFlowStateRequest, FlowState>
    {
        public FlowStateService StateService { get; }

        public InitFlowStateRequestHandler(FlowStateService stateService)
        {
            StateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
        }

        public Task<FlowResult<FlowState>> DoAsync(InitFlowStateRequest request)
        {
            return StateService.CreateFlowStateAsync(request);
        }
    }
}
