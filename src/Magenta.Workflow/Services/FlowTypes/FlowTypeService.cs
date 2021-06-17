using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.Base;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.UseCases.InitFlowType;

namespace Magenta.Workflow.Services.FlowTypes
{
    public class FlowTypeService : BaseService
    {
        public FlowTypeService(IStateManager stateManager) : base(stateManager)
        {

        }

        public async Task<FlowResult<FlowType>> CreateFlowTypeAsync(InitFlowTypeRequest initRequest)
        {
            var set = StateManager.GetFlowSet<FlowType>();
            var entity = FlowEntity.InitializeType(new FlowType()
            {
                Name = initRequest.Name,
            });
            var resultTask = await set.CreateAsync(entity);
            return FlowResult<FlowType>.Successful(resultTask);
        }
    }
}