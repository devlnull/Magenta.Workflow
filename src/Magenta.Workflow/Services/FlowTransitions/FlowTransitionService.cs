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

        public async Task<FlowResult<FlowTransition>> CreateFlowTransitionAsync(InitFlowTransitionRequest request)
        {
            var set = StateManager.GetFlowSet<FlowTransition>();
            var stateSet = StateManager.GetFlowSet<FlowState>();

            var source = await stateSet.GetByIdAsync(request.SourceId);
            if (source == null)
                return FlowResult<FlowTransition>
                    .Failed(new FlowError(FlowErrors.ItemNotFound, nameof(source)));

            var destination = await stateSet.GetByIdAsync(request.DestinationId);
            if (destination == null)
                return FlowResult<FlowTransition>
                    .Failed(new FlowError(FlowErrors.ItemNotFound, nameof(destination)));

            var entity = FlowEntity.InitializeType(new FlowTransition()
            {
                Name = request.Name,
                Title = request.Title,
                SourceId = source.Id,
                DestinationId = destination.Id,
                TransitionType = request.TransitionType,
                TypeId = source.TypeId,
            });

            var result = await set.CreateAsync(entity);

            return FlowResult<FlowTransition>.Successful(result);
        }
    }

}