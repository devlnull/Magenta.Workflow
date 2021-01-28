using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.Base;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Managers.States;

namespace Magenta.Workflow.Services.FlowTypes
{
    public class FlowTypeService : BaseService
    {
        public FlowTypeService(IStateManager stateManager) : base(stateManager)
        {

        }

        public async Task<FlowResult<FlowType>> CreateFlowTypeAsync<TEntity, TPayloadEntity>(string name)
        {
            var set = _stateManager.GetFlowSet<FlowType>();
            var entity = FlowEntity.InitializeType(new FlowType()
            {
                EntityType = typeof(TEntity).Name,
                EntityPayloadType = typeof(TPayloadEntity).Name,
                Name = name,
            });
            var resultTask = await set.CreateAsync(entity);
            return FlowResult<FlowType>.Successful(resultTask);
        }
    }
}