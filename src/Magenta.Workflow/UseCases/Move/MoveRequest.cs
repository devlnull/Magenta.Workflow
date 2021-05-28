using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowSteps;
using System;
using System.Threading.Tasks;

namespace Magenta.Workflow.UseCases.Move
{
    public class MoveRequest : IFlowRequest<MoveModel, FlowStep>
    {
        private FlowStepService StepService { get; }

        public MoveRequest(FlowStepService transitionService)
        {
            StepService = transitionService ?? throw new ArgumentNullException(nameof(transitionService));
        }

        public Task<FlowResult<FlowStep>> DoAsync(MoveModel model)
        {
            return StepService.CreateFlowStepAsync(model);
        }
    }
}
