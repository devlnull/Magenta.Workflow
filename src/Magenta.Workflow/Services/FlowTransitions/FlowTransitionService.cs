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
    public class FlowTransitionService : BaseService<InitFlowTransitionModel, FlowResult<FlowTransition>>
    {
        public FlowTransitionService(IStateManager stateManager) : base(stateManager)
        {
        }

        public override async Task<FlowResult<FlowTransition>> HandleRequestAsync(
            InitFlowTransitionModel request)
        {
            var set = _stateManager.GetFlowSet<FlowTransition>();
            var stateSet = _stateManager.GetFlowSet<FlowState>();

            var source = await stateSet.GetByGuidAsync(request.SourceGuidRow);
            if (source == null)
                return FlowResult<FlowTransition>
                    .Failed(new FlowError(FlowErrors.ITEM_NOTFOUND, nameof(source)));

            var destination = await stateSet.GetByGuidAsync(request.DestinationGuidRow);
            if (destination == null)
                return FlowResult<FlowTransition>
                    .Failed(new FlowError(FlowErrors.ITEM_NOTFOUND, nameof(destination)));

            var entity = FlowEntity.InitializeType(new FlowTransition()
            {
                Name = request.Name,
                Title = request.Title,
                SourceId = source.Id,
                DestinationId = destination.Id,
                TransitionType = request.TransitionType,
            });

            var result = await set.CreateAsync(entity);

            return FlowResult<FlowTransition>.Successful(result);
        }
    }

}