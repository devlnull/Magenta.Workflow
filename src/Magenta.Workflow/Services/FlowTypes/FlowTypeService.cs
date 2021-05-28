using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.Base;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.UseCases.InitFlowType;

namespace Magenta.Workflow.Services.FlowTypes
{
    public class FlowTypeService : BaseService<InitFlowTypeModel, FlowResult<FlowType>>
    {
        public FlowTypeService(IStateManager stateManager) : base(stateManager)
        {

        }


        public override async Task<FlowResult<FlowType>> HandleRequestAsync(InitFlowTypeModel request)
        {
            var set = _stateManager.GetFlowSet<FlowType>();
            var entity = FlowEntity.InitializeType(new FlowType()
            {
                EntityType = request.EntityType.Name,
                EntityPayloadType = request.EntityPayloadType.Name,
                Name = request.Name,
            });
            var resultTask = await set.CreateAsync(entity);
            return FlowResult<FlowType>.Successful(resultTask);
        }
    }
}