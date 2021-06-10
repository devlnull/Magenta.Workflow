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

        public MoveRequest(FlowStepService stepService)
        {
            StepService = stepService ?? throw new ArgumentNullException(nameof(stepService));
        }

        public async Task<FlowResult<FlowStep>> DoAsync(MoveModel model)
        {
            //disable current flag of previous state
            var disableCurrentStepResult = await StepService.DisableCurrentStepAsync(model.InstanceId);
            //create new step with current flag true
            var stepResult = await StepService.CreateFlowStepAsync(model);

            return disableCurrentStepResult.Merge(stepResult);
            //return FlowResult<FlowStep>.Success;
        }
    }
}
