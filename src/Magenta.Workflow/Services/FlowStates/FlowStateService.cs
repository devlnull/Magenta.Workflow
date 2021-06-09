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
    public class FlowStateService : BaseService
    {
        public FlowStateService(IStateManager stateManager) : base(stateManager)
        {
        }

        public async Task<FlowResult<FlowState>> CreateFlowStateAsync(InitFlowStateModel model)
        {
            var set = StateManager.GetFlowSet<FlowState>();
            var typeSet = StateManager.GetFlowSet<FlowType>();

            var type = await typeSet.GetByIdAsync(model.TypeId);
            if (type == null)
                return FlowResult<FlowState>
                    .Failed(new FlowError(FlowErrors.ItemNotfound, nameof(type)));

            var entity = FlowEntity.InitializeType(new FlowState()
            {
                Name = model.Name,
                Title = model.Title,
                StateType = (byte)model.StateType,
                TypeId = type.Id,
            });

            var result = await set.CreateAsync(entity);

            return FlowResult<FlowState>.Successful(result);
        }
    }
}