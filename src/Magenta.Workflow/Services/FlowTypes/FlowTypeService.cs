using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Entities.Flows;
using Magenta.Workflow.Services.Base;
using System.Threading.Tasks;

namespace Magenta.Workflow.Services.FlowTypes
{
    public class FlowTypeService : BaseService
    {
        public async Task<FlowTaskResult<FlowType>> CreateFlowTypeAsync<TEntity, TPayloadEntity>(string name)
        {
            var set = _stateManager.GetFlowSet<FlowType>();
            var entity = new FlowType()
            {
                EntityType = typeof(TEntity).Name,
                EntityPayloadType = typeof(TPayloadEntity).Name,
                Name = name,
            };
            entity = InitializeType(entity);
            var resultTask = await set.CreateAsync(entity);
            return FlowTaskResult<FlowType>.Successful(resultTask);
        }
    }
}