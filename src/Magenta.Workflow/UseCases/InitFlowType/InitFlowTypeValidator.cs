using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Core.Validators;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.UseCases.InitFlowType
{
    public class InitFlowTypeValidator : IFlowValidator<InitFlowTypeRequest>
    {
        public async Task<FlowResult> ValidateAsync(IStateManager stateManager, InitFlowTypeRequest request)
        {
            FlowResult result = new FlowResult();

            if (string.IsNullOrWhiteSpace(request.Name))
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsRequired, args: nameof(request.Name)));

            if (await DuplicateNameExistAsync(stateManager, request))
                result.Errors.Add(new FlowError(FlowMessages.ItemAlreadyexist, args: "Name"));

            return result;
        }

        private async Task<bool> DuplicateNameExistAsync(IStateManager stateManager, InitFlowTypeRequest request)
        {
            var types = stateManager.GetFlowSet<FlowType>();
            var item = await types.FirstOrDefaultAsync(x => x.Name.Equals(request.Name));
            return item != null;
        }
    }
}
