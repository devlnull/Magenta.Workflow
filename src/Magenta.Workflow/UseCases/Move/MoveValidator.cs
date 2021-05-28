using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Core.Validators;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;
using System.Threading.Tasks;

namespace Magenta.Workflow.UseCases.Move
{
    public class MoveValidator : IFlowValidator<MoveModel>
    {
        public Task<FlowResult> ValidateAsync(IStateManager stateManager, 
            MoveModel model)
        {
            FlowResult result = new FlowResult();

            if (model.InstanceId.GuidIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.SERVICE_ISEMPTY, args: nameof(model.Comment)));

            if (model.TransitionId.GuidIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.SERVICE_ISREQUIRED, args: nameof(model.TransitionId)));

            if (model.IdentityId.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.SERVICE_ISREQUIRED, args: nameof(model.IdentityId)));

            if (model.Comment.StringIsEmpty())
                result.Warns.Add(new FlowWarn(FlowErrors.SERVICE_ISEMPTY, args: nameof(model.Comment)));

            if (model.Payload.StringIsEmpty())
                result.Warns.Add(new FlowWarn(FlowErrors.SERVICE_ISEMPTY, args: nameof(model.Payload)));

            return Task.FromResult(result);
        }
    }
}
