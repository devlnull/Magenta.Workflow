using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowTransitions;

namespace Magenta.Workflow.UseCases.InitFlowTransition
{
    public class InitFlowTransitionRequest : IFlowRequest<InitFlowTransitionModel, FlowTransition>
    {
        private FlowTransitionService TransitionService { get; }

        public InitFlowTransitionRequest(FlowTransitionService transitionService)
        {
            TransitionService = transitionService ?? throw new ArgumentNullException(nameof(transitionService));
        }

        public Task<FlowResult<FlowTransition>> DoAsync(InitFlowTransitionModel model)
        {
            return TransitionService.HandleRequestAsync(model);
        }
    }
}
