using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowTransitions;

namespace Magenta.Workflow.UseCases.InitFlowTransition
{
    public class InitFlowTransitionRequestHandler : IFlowRequestHandler<InitFlowTransitionRequest, FlowTransition>
    {
        private FlowTransitionService TransitionService { get; }

        public InitFlowTransitionRequestHandler(FlowTransitionService transitionService)
        {
            TransitionService = transitionService ?? throw new ArgumentNullException(nameof(transitionService));
        }

        public Task<FlowResult<FlowTransition>> DoAsync(InitFlowTransitionRequest request)
        {
            return TransitionService.CreateFlowTransitionAsync(request);
        }
    }
}
