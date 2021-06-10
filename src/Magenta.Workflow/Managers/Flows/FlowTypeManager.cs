using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.UseCases.InitFlowType;

namespace Magenta.Workflow.Managers.Flows
{
    public partial class FlowManager
    {

        public async Task<FlowResult<FlowType>> InitFlowTypeAsync(InitFlowTypeModel initModel)
        {
            var result = await HandleRequestAsync(new InitFlowTypeRequest(TypeService), initModel);
            return result;
        }

    }
}
