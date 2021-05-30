using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Services.Base;
using Magenta.Workflow.UseCases.Move;
using Magenta.Workflow.Utilities;
using System;
using System.Threading.Tasks;

namespace Magenta.Workflow.Services.FlowSteps
{
    public class FlowStepService : BaseService
    {
        public FlowStepService(IStateManager stateManager) : base(stateManager)
        {
        }

        public async Task<FlowResult<FlowStep>> CreateFlowStepAsync(MoveModel model)
        {
            var set = _stateManager.GetFlowSet<FlowStep>();
            var instanceSet = _stateManager.GetFlowSet<FlowInstance>();
            var transitionSet = _stateManager.GetFlowSet<FlowTransition>();

            var instance = await instanceSet.GetByIdAsync(model.InstanceId);
            if (instance == null)
                return FlowResult<FlowStep>
                    .Failed(new FlowError(FlowErrors.ITEM_NOTFOUND, nameof(instance)));

            var transition = await transitionSet.GetByIdAsync(model.TransitionId);
            if (transition == null)
                return FlowResult<FlowStep>
                    .Failed(new FlowError(FlowErrors.ITEM_NOTFOUND, nameof(transition)));

            var entity = FlowEntity.InitializeType(new FlowStep()
            {
                InstanceId = instance.Id,
                IsCurrent = true,
                TransitionId = transition.Id,
                Payload = model.Payload,
                Comment = model.Comment,
            });

            var result = await set.CreateAsync(entity);

            return FlowResult<FlowStep>.Successful(result);
        }

        public async Task<FlowResult<FlowStep>> DisableCurrentStepAsync(Guid instanceId)
        {
            var set = _stateManager.GetFlowSet<FlowStep>();
            var instanceSet = _stateManager.GetFlowSet<FlowInstance>();

            var instance = await instanceSet.GetByIdAsync(instanceId);
            if (instance == null)
                return FlowResult<FlowStep>
                    .Failed(new FlowError(FlowErrors.ITEM_NOTFOUND, nameof(instance)));

            var currentStep = await set.FirstOrDefaultAsync(x => x.InstanceId.Equals(instanceId) && x.IsCurrent);
            currentStep.IsCurrent = false;
            var updateResult = await set.UpdateAsync(currentStep);

            return FlowResult<FlowStep>.Successful(updateResult);
        }
    }
}