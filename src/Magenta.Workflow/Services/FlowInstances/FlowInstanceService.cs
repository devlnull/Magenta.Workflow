using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Entities.Base;
using Magenta.Workflow.Entities.Flows;
using Magenta.Workflow.Managers.States.Abstracts;
using Magenta.Workflow.Services.Base;
using Magenta.Workflow.Services.FlowInstances.Models;
using System.Threading.Tasks;

namespace Magenta.Workflow.Services.FlowInstances
{
    public class FlowInstanceService : BaseService
    {
        public FlowInstanceService(IStateManager stateManager) : base(stateManager)
        {

        }

        public async Task<FlowResult<FlowInstance>> CreateFlowInstanceAsync(InstanceCreateModel model)
        {
            var set = _stateManager.GetFlowSet<FlowInstance>();
            var typeSet = _stateManager.GetFlowSet<FlowType>();

            var type = await typeSet.GetByGuidAsync(model.TypeId);
            if (type == null)
                return FlowResult<FlowInstance>.Failed(new[] { new FlowError("Flow type not found.") });

            var entity = FlowEntity.InitializeType(new FlowInstance()
            {
                Title = model.Title,
                Payload = model.Payload,
                TypeId = type.Id,
                InitializerId = model.InitializerId,
                AccessPhrase = model.AccessPhrase,
            });

            var result = await set.CreateAsync(entity);

            return FlowResult<FlowInstance>.Successful(result);
        }
    }
}