using System.Threading.Tasks;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Core.Validators;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.UseCases.InitFlow
{
    public class InitFlowValidator : IFlowValidator<InitFlowRequest>
    {
        public Task<FlowResult> ValidateAsync(IStateManager stateManager, InitFlowRequest request)
        {
            FlowResult result = new FlowResult();

            if (request.Title.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsRequired, args: nameof(request.Title)));

            if (request.InitializerId.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsRequired, args: nameof(request.InitializerId)));

            if (request.AccessPhrase.StringIsEmpty())
                result.Warns.Add(new FlowWarn(FlowErrors.ServiceIsEmpty, args: nameof(request.AccessPhrase)));

            if (request.Payload.StringIsEmpty())
                result.Warns.Add(new FlowWarn(FlowMessages.ItemAlreadyexist, args: nameof(request.Payload)));

            return Task.FromResult(result);
        }
    }
}
