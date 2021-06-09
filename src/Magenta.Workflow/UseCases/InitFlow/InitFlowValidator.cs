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

            if (model.Title.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsrequired, args: nameof(model.Title)));

            if (model.InitializerId.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsrequired, args: nameof(model.InitializerId)));

            if (model.AccessPhrase.StringIsEmpty())
                result.Warns.Add(new FlowWarn(FlowErrors.ServiceIsempty, args: nameof(model.AccessPhrase)));

            if (model.Payload.StringIsEmpty())
                result.Warns.Add(new FlowWarn(FlowMessages.ItemAlreadyexist, args: nameof(model.Payload)));

            return Task.FromResult(result);
        }
    }
}
