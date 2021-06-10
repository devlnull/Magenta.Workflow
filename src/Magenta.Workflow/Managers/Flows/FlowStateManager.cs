using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.UseCases.InitFlowState;

namespace Magenta.Workflow.Managers.Flows
{
    public partial class FlowManager
    {

        public async Task<FlowResult<FlowState>> InitFlowStateAsync(InitFlowStateModel initModel)
        {
            var result = await HandleRequestAsync(new InitFlowStateRequest(StateService), initModel);
            return result;
        }

    }
}