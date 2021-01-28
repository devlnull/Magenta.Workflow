using System.Threading.Tasks;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Core.Validators;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.UseCases.InitFlow
{
    public class InitFlowValidator : IFlowValidator<InitFlowModel>
    {
        public Task<FlowResult> ValidateAsync(IStateManager stateManager, InitFlowModel model)
        {
            FlowResult result = new FlowResult();

            if (string.IsNullOrWhiteSpace(model.Title))
                result.Errors.Add(new FlowError(FlowErrors.SERVICE_ISREQUIRED, args: nameof(model.Title)));

            if (string.IsNullOrWhiteSpace(model.InitializerId))
                result.Errors.Add(new FlowError(FlowErrors.SERVICE_ISREQUIRED, args: nameof(model.InitializerId)));

            if (string.IsNullOrWhiteSpace(model.AccessPhrase))
                result.Warns.Add(new FlowWarn(FlowErrors.SERVICE_ISEMPTY, args: nameof(model.AccessPhrase)));

            if (string.IsNullOrWhiteSpace(model.Payload))
                result.Warns.Add(new FlowWarn(FlowMessages.ITEM_ALREADYEXIST, args: nameof(model.Payload)));

            return Task.FromResult(result);
        }
    }
}
