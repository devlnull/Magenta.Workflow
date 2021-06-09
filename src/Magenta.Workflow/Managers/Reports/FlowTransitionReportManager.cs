using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.Managers.Reports
{
    public partial class FlowReportManager
    {

        public async Task<FlowResult<IEnumerable<FlowTransition>>> GetInstanceTransitionsAsync(Guid instanceId)
        {
            //Get current instance
            if (instanceId.GuidIsEmpty())
                throw new ArgumentNullException(nameof(instanceId));

            var instanceSet = StateManager.GetFlowSet<FlowInstance>();

            var targetInstance = await instanceSet.FirstOrDefaultAsync(x => x.Id.Equals(instanceId));
            if (targetInstance == null)
                return FlowResult<IEnumerable<FlowTransition>>
                    .Failed(new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowInstance)));

            //Get current instance state
            var stepSet = StateManager.GetFlowSet<FlowStep>();

            var currentStep = await stepSet
                .FirstOrDefaultAsync(x => x.InstanceId.Equals(targetInstance.Id) && x.IsCurrent);

            if (currentStep == null)
                return FlowResult<IEnumerable<FlowTransition>>
                    .Failed(new FlowError(FlowErrors.InstanceHasnostep));

            //Get current step transition
            var transitionSet = StateManager.GetFlowSet<FlowTransition>();

            var currentTransition = await transitionSet
                .FirstOrDefaultAsync(x => x.Id.Equals(currentStep.TransitionId));

            //Get current state
            var stateSet = StateManager.GetFlowSet<FlowState>();

            var currentState = await stateSet.FirstOrDefaultAsync(x => x.Id.Equals(currentTransition.DestinationId));

            //Get current state transitions
            var transitions = await transitionSet.GetAllAsync(x => x.SourceId.Equals(currentState.Id));

            return FlowResult<IEnumerable<FlowTransition>>.Successful(transitions);
        }


    }
}