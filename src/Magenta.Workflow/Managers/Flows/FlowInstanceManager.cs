using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.UseCases.InitFlow;

namespace Magenta.Workflow.Managers.Flows
{
    public partial class FlowManager
    {

        public async Task<FlowResult<FlowInstance>> InitFlowAsync(InitFlowModel initModel)
        {
            var result = await HandleRequestAsync(new InitFlowRequest(StateManager, InstanceService, StepService),
                initModel);
            return result;
        }

    }
}
