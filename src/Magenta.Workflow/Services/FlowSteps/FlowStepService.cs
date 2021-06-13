using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Services.Base;
using Magenta.Workflow.UseCases.Move;
using Magenta.Workflow.Utilities;
using System;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Magenta.Workflow.Services.FlowSteps
{
    public class FlowStepService : BaseService
    {
        public FlowStepService(IStateManager stateManager) : base(stateManager)
        {
        }

        public async Task<FlowResult<FlowStep>> CreateFlowStepAsync(MoveModel model)
        {
            var set = StateManager.GetFlowSet<FlowStep>();
            var instanceSet = StateManager.GetFlowSet<FlowInstance>();
            var transitionSet = StateManager.GetFlowSet<FlowTransition>();
            var stateSet = StateManager.GetFlowSet<FlowState>();

            var instance = await instanceSet.GetByIdAsync(model.InstanceId);
            if (instance == null)
                return FlowResult<FlowStep>
                    .Failed(new FlowError(FlowErrors.ItemNotFound, nameof(instance)));

            var transition = await transitionSet.GetByIdAsync(model.TransitionId);
            if (transition == null)
                return FlowResult<FlowStep>
                    .Failed(new FlowError(FlowErrors.ItemNotFound, nameof(transition)));

            var state = await stateSet.GetByIdAsync(transition.DestinationId);
            if(state == null)
                return FlowResult<FlowStep>
                    .Failed(new FlowError(FlowErrors.ItemNotFound, nameof(state)));

            var entity = FlowEntity.InitializeType(new FlowStep()
            {
                InstanceId = instance.Id,
                IsCurrent = true,
                TransitionId = transition.Id,
                Payload = model.Payload,
                Comment = model.Comment,
                CurrentStateName = state.Name,
                CurrentStateTitle = state.Title,
                CurrentStateType = state.StateType,
            });

            var result = await set.CreateAsync(entity);

            return FlowResult<FlowStep>.Successful(result);
        }

        public async Task<FlowResult<FlowStep>> DisableCurrentStepAsync(Guid instanceId)
        {
            var set = StateManager.GetFlowSet<FlowStep>();
            var instanceSet = StateManager.GetFlowSet<FlowInstance>();

            var instance = await instanceSet.GetByIdAsync(instanceId);
            if (instance == null)
                return FlowResult<FlowStep>
                    .Failed(new FlowError(FlowErrors.ItemNotFound, nameof(instance)));

            var currentStep = await set.FirstOrDefaultAsync(x => x.InstanceId.Equals(instanceId) && x.IsCurrent);
            currentStep.IsCurrent = false;
            var updateResult = await set.UpdateAsync(currentStep);

            return FlowResult<FlowStep>.Successful(updateResult);
        }
    }
}