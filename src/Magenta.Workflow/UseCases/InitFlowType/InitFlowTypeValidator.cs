using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Core.Validators;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.UseCases.InitFlowType
{
    public class InitFlowTypeValidator : IFlowValidator<InitFlowTypeModel>
    {
        public async Task<FlowResult> ValidateAsync(IStateManager stateManager, InitFlowTypeModel model)
        {
            FlowResult result = new FlowResult();

            if (string.IsNullOrWhiteSpace(model.Name))
                result.Errors.Add(new FlowError(FlowErrors.SERVICE_ISREQUIRED, args: nameof(model.Name)));

            if (model.EntityPayloadType == null)
                result.Errors.Add(new FlowError(FlowErrors.SERVICE_ISREQUIRED, args: nameof(model.EntityPayloadType)));

            if (model.EntityType == null)
                result.Errors.Add(new FlowError(FlowErrors.SERVICE_ISREQUIRED, args: nameof(model.EntityType)));

            if (await DuplicateNameExistAsync(stateManager, model))
                result.Errors.Add(new FlowError(FlowMessages.ITEM_ALREADYEXIST, args: "Type"));

            return result;
        }

        private async Task<bool> DuplicateNameExistAsync(IStateManager stateManager, InitFlowTypeModel model)
        {
            var types = stateManager.GetFlowSet<FlowType>();
            var exist = await types.AnyAsync(x => x.Name.Equals(model.Name));
            return exist;
        }
    }
}
