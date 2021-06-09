using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.UseCases.Move;

namespace Magenta.Workflow.Managers.Flows
{
    public partial class FlowManager
    {

        public async Task<FlowResult<FlowStep>> MoveAsync(MoveModel moveModel)
        {
            var result = await HandleRequestAsync(new MoveRequest(StepService), moveModel);
            return result;
        }

    }
}