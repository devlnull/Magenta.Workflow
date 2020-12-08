using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Entities.Base;
using Magenta.Workflow.Entities.Flows;
using Magenta.Workflow.Managers.States.Abstracts;
using Magenta.Workflow.Services.Base;
using System.Threading.Tasks;

namespace Magenta.Workflow.Services.FlowTypes
{
    public class FlowTypeService : BaseService
    {
        public FlowTypeService(IStateManager stateManager) : base(stateManager)
        {

        }

        public async Task<FlowTaskResult<FlowType>> CreateFlowTypeAsync<TEntity, TPayloadEntity>(string name)
        {
            var set = _stateManager.GetFlowSet<FlowType>();
            var entity = FlowEntity.InitializeType(new FlowType()
            {
                EntityType = typeof(TEntity).Name,
                EntityPayloadType = typeof(TPayloadEntity).Name,
                Name = name,
            });
            var resultTask = await set.CreateAsync(entity);
            return FlowTaskResult<FlowType>.Successful(resultTask);
        }
    }
}