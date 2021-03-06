using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Services.Base;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.UseCases.InitFlow;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.Services.FlowInstances
{
    public class FlowInstanceService : BaseService
    {
        public FlowInstanceService(IStateManager stateManager) : base(stateManager)
        {

        }

        public async Task<FlowResult<FlowInstance>> CreateFlowInstanceAsync(InitFlowModel model)
        {
            var set = StateManager.GetFlowSet<FlowInstance>();
            var typeSet = StateManager.GetFlowSet<FlowType>();

            var type = await typeSet.GetByIdAsync(model.TypeId);
            if (type == null)
                return FlowResult<FlowInstance>
                    .Failed(new FlowError(FlowErrors.ItemNotFound, nameof(type)));

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