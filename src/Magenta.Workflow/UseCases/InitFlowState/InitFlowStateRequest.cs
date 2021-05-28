using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowStates;

namespace Magenta.Workflow.UseCases.InitFlowState
{
    public class InitFlowStateRequest : IFlowRequest<InitFlowStateModel, FlowState>
    {
        public FlowStateService StateService { get; }

        public InitFlowStateRequest(FlowStateService stateService)
        {
            StateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
        }

        public Task<FlowResult<FlowState>> DoAsync(InitFlowStateModel model)
        {
            return StateService.HandleRequestAsync(model);
        }
    }
}
