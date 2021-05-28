using Magenta.Workflow.UseCases.InitFlowState;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Services.Base;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.Services.FlowStates
{
    public class FlowStateService : BaseService<InitFlowStateModel, FlowResult<FlowState>>
    {
        public FlowStateService(IStateManager stateManager) : base(stateManager)
        {
        }

        public override async Task<FlowResult<FlowState>> HandleRequestAsync(
            InitFlowStateModel request)
        {
            var set = _stateManager.GetFlowSet<FlowState>();
            var typeSet = _stateManager.GetFlowSet<FlowType>();

            var type = await typeSet.GetByGuidAsync(request.TypeGuidRow);
            if (type == null)
                return FlowResult<FlowState>
                    .Failed(new FlowError(FlowErrors.ITEM_NOTFOUND, nameof(type)));

            var entity = FlowEntity.InitializeType(new FlowState()
            {
                Name = request.Name,
                Title = request.Title,
                StateType = (byte)request.StateType,
                TypeId = type.Id,
            });

            var result = await set.CreateAsync(entity);

            return FlowResult<FlowState>.Successful(result);
        }
    }
}