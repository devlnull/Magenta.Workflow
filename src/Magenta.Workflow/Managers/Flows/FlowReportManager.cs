using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Magenta.Workflow.Managers.Flows
{
    public class FlowReportManager : IFlowReportManager
    {
        public FlowReportManager(IStateManager stateManager)
        {
            StateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
        }

        public IStateManager StateManager { get; set; }


        #region Transition Reports

        public async Task<FlowResult<IEnumerable<FlowTransition>>> GetInstanceTransitionsAsync(Guid instanceId)
        {
            //Get current instance
            if (instanceId.GuidIsEmpty())
                throw new ArgumentNullException(nameof(instanceId));

            var instanceSet = StateManager.GetFlowSet<FlowInstance>();

            var targetInstance = await instanceSet.FirstOrDefaultAsync(x => x.Id.Equals(instanceId));
            if (targetInstance == null)
                return FlowResult<IEnumerable<FlowTransition>>
                    .Failed(new FlowError(FlowErrors.ITEM_NOTFOUND, args: nameof(FlowInstance)));

            //Get current instance state
            var stepSet = StateManager.GetFlowSet<FlowStep>();

            var currentStep = await stepSet
                .FirstOrDefaultAsync(x => x.InstanceId.Equals(targetInstance.Id) && x.IsCurrent);

            if(currentStep == null)
                return FlowResult<IEnumerable<FlowTransition>>
                    .Failed(new FlowError(FlowErrors.INSTANCE_HASNOSTEP));

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

        #endregion Transition Reports

        #region Steps Reports

        public async Task<FlowResult<IEnumerable<FlowStep>>> GetInstanceStepsAsync(Guid instanceId)
        {
            //Get current instance
            if (instanceId.GuidIsEmpty())
                throw new ArgumentNullException(nameof(instanceId));

            var instanceSet = StateManager.GetFlowSet<FlowInstance>();

            var targetInstance = await instanceSet.FirstOrDefaultAsync(x => x.Id.Equals(instanceId));
            if (targetInstance == null)
                return FlowResult<IEnumerable<FlowStep>>
                    .Failed(new FlowError(FlowErrors.ITEM_NOTFOUND, args: nameof(FlowInstance)));

            //Get all steps
            var stepSet = StateManager.GetFlowSet<FlowStep>();
            var steps = await stepSet.GetAllAsync(x => x.InstanceId.Equals(targetInstance.Id));

            return FlowResult<IEnumerable<FlowStep>>.Successful(steps);
        }

        #endregion Steps Reports

        #region Instance Reports

        #endregion Instance Reports

        #region Identity Reports

        #endregion Identity Reports

        #region Type Reports

        #endregion Type Reports

        #region Overview Reports

        // Number of instances, types, users, roles(groups)
        //Current status of flow

        #endregion Overview Reports

        #region Complex Reports

        // Flow history

        #endregion Complex Reports
    }
}
