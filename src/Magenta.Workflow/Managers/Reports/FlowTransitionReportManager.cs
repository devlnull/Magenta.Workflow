using System;
using System.Collections.Generic;
using System.Linq;
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
                    .Failed(new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowInstance)));

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

        public async Task<FlowResult<IEnumerable<FlowTransition>>> GetSourceTransitionsAsync(Guid sourceId)
        {
            //Get current instance
            if (sourceId.GuidIsEmpty())
                throw new ArgumentNullException(nameof(sourceId));

            var stateSet = StateManager.GetFlowSet<FlowState>();

            var targetSource = await stateSet
                .FirstOrDefaultAsync(x => x.Id.Equals(sourceId));

            if (targetSource == null)
                return FlowResult<IEnumerable<FlowTransition>>
                    .Failed(new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowState)));

            var transitionSet = StateManager.GetFlowSet<FlowTransition>();
            var typeSet = StateManager.GetFlowSet<FlowType>();
            var reasonSet = StateManager.GetFlowSet<FlowTransitionReason>();

            var query = from transition in transitionSet.GetAll()
                        join type in typeSet.GetAll() on transition.TypeId equals type.Id
                        join source in stateSet.GetAll() on transition.SourceId equals source.Id
                        join destination in stateSet.GetAll() on transition.DestinationId equals destination.Id
                        where transition.SourceId == sourceId
                        select new FlowTransition()
                        {
                            Id = transition.Id,
                            Type = type,
                            CreatedAt = transition.CreatedAt,
                            ModifiedAt = transition.ModifiedAt,
                            Deleted = transition.Deleted,
                            TypeId = transition.TypeId,
                            Name = transition.Name,
                            SourceId = transition.SourceId,
                            DestinationId = transition.DestinationId,
                            Destination = destination,
                            IsAutomatic = transition.IsAutomatic,
                            Source = source,
                            Title = transition.Title,
                            TransitionType = transition.TransitionType
                        };

            var transitions = await transitionSet.ToListAsync(query);

            return FlowResult<IEnumerable<FlowTransition>>.Successful(transitions);
        }

    }
}