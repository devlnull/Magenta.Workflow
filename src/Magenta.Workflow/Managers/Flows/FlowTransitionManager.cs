using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.UseCases.InitFlowTransition;

namespace Magenta.Workflow.Managers.Flows
{
    public partial class FlowManager
    {

        public async Task<FlowResult<FlowTransition>> InitFlowTransitionAsync(InitFlowTransitionModel initModel)
        {
            var result = await HandleRequestAsync(new InitFlowTransitionRequest(TransitionService), initModel);
            return result;
        }

    }
}