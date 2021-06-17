using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.FlowSteps;
using System;
using System.Threading.Tasks;

namespace Magenta.Workflow.UseCases.Move
{
    public class MoveRequestHandler : IFlowRequestHandler<MoveRequest, FlowStep>
    {
        private FlowStepService StepService { get; }

        public MoveRequestHandler(FlowStepService stepService)
        {
            StepService = stepService ?? throw new ArgumentNullException(nameof(stepService));
        }

        public async Task<FlowResult<FlowStep>> DoAsync(MoveRequest request)
        {
            //disable current flag of previous state
            var disableCurrentStepResult = await StepService.DisableCurrentStepAsync(request.InstanceId);
            //create new step with current flag true
            var stepResult = await StepService.CreateFlowStepAsync(request);

            return disableCurrentStepResult.Merge(stepResult);
            //return FlowResult<FlowStep>.Success;
        }
    }
}
