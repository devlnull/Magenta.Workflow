using System.Threading.Tasks;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Services.Base;
using Magenta.Workflow.UseCases.InitFlowTransition;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.Services.FlowTransitions
{
    public class FlowTransitionService : BaseService
    {
        public FlowTransitionService(IStateManager stateManager) : base(stateManager)
        {
        }

        public async Task<FlowResult<FlowTransition>> CreateFlowTransitionAsync(InitFlowTransitionModel model)
        {
            var set = _stateManager.GetFlowSet<FlowTransition>();
            var stateSet = _stateManager.GetFlowSet<FlowState>();

            var source = await stateSet.GetByIdAsync(model.SourceId);
            if (source == null)
                return FlowResult<FlowTransition>
                    .Failed(new FlowError(FlowErrors.ITEM_NOTFOUND, nameof(source)));

            var destination = await stateSet.GetByIdAsync(model.DestinationId);
            if (destination == null)
                return FlowResult<FlowTransition>
                    .Failed(new FlowError(FlowErrors.ITEM_NOTFOUND, nameof(destination)));

            var entity = FlowEntity.InitializeType(new FlowTransition()
            {
                Name = model.Name,
                Title = model.Title,
                SourceId = source.Id,
                DestinationId = destination.Id,
                TransitionType = model.TransitionType,
                TypeId = source.TypeId,
            });

            var result = await set.CreateAsync(entity);

            return FlowResult<FlowTransition>.Successful(result);
        }
    }

}