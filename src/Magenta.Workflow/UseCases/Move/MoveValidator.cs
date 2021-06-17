using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Core.Validators;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace Magenta.Workflow.UseCases.Move
{
    public class MoveValidator : IFlowValidator<MoveRequest>
    {
        public async Task<FlowResult> ValidateAsync(IStateManager stateManager,
            MoveRequest request)
        {
            FlowResult result = new FlowResult();

            if (request.InstanceId.GuidIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsEmpty, args: nameof(request.Comment)));

            if (request.TransitionId.GuidIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsRequired, args: nameof(request.TransitionId)));

            if (request.IdentityId.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsRequired, args: nameof(request.IdentityId)));

            if (request.Comment.StringIsEmpty())
                result.Warns.Add(new FlowWarn(FlowErrors.ServiceIsEmpty, args: nameof(request.Comment)));

            if (request.Payload.StringIsEmpty())
                result.Warns.Add(new FlowWarn(FlowErrors.ServiceIsEmpty, args: nameof(request.Payload)));

            var validateSourceDestinationResult = await ValidateSourceDestinationAsync(stateManager, request);
            var validatePossibleMoveResult = await ValidatePossibleMoveAsync(stateManager, request);
            var validateInstanceResult = await ValidateInstanceAsync(stateManager, request);

            return result.Merge(validateInstanceResult)
                .Merge(validatePossibleMoveResult)
                .Merge(validateSourceDestinationResult);
        }

        private async Task<FlowResult> ValidatePossibleMoveAsync(IStateManager stateManager, MoveRequest request)
        {
            var instanceSet = stateManager.GetFlowSet<FlowInstance>();

            var instance = await instanceSet.GetByIdAsync(request.InstanceId);
            if (instance == null)
                return FlowResult.Failed(new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowInstance)));

            var stepSet = stateManager.GetFlowSet<FlowStep>();
            var instanceCurrentStep = await stepSet
                .FirstOrDefaultAsync(x => x.InstanceId.Equals(instance.Id) && x.IsCurrent);

            var transitionSet = stateManager.GetFlowSet<FlowTransition>();
            var transition = await transitionSet.GetByIdAsync(request.TransitionId);
            if (transition == null)
                return FlowResult.Failed(
                    new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowTransition)));

            var instanceLastTransition = await transitionSet.GetByIdAsync(instanceCurrentStep.TransitionId);
            if (instanceCurrentStep == null)
                return FlowResult.Failed(new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowTransition)));

            var stateSet = stateManager.GetFlowSet<FlowState>();
            var state = await stateSet.GetByIdAsync(instanceLastTransition.DestinationId);
            if (state == null)
                return FlowResult.Failed(new FlowError(FlowErrors.ItemNotFound, args: nameof(state)));

            var possibleTransitions = await transitionSet.GetAllAsync(x => x.SourceId == state.Id);
            if (possibleTransitions.Select(x => x.Id).Contains(request.TransitionId) == false)
            {
                string sourceParam = transition.SourceId.HasValue ?
                    transition.SourceId.Value.ToString() : FlowErrors.StateNull;
                string destinationParam = transition.DestinationId.ToString();

                return FlowResult.Failed(new FlowError(FlowErrors.MoveImpossibleTransition,
                    sourceParam, destinationParam));
            }

            return FlowResult.Success;
        }

        private async Task<FlowResult> ValidateInstanceAsync(IStateManager stateManager, MoveRequest request)
        {
            var instanceSet = stateManager.GetFlowSet<FlowInstance>();

            var instance = await instanceSet.GetByIdAsync(request.InstanceId);
            if (instance == null)
                return FlowResult.Failed(
                    new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowInstance)));

            if (instance.Active == false)
                return FlowResult.Failed(new FlowError(FlowErrors.InstanceIsInactive));

            return FlowResult.Success;
        }

        private async Task<FlowResult> ValidateSourceDestinationAsync(IStateManager stateManager,
            MoveRequest request)
        {
            var transitionSet = stateManager.GetFlowSet<FlowTransition>();
            var transition = await transitionSet.GetByIdAsync(request.TransitionId);
            if (transition == null)
                return FlowResult.Failed(
                    new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowTransition)));

            var stateSet = stateManager.GetFlowSet<FlowState>();

            if (transition.SourceId.HasValue == true)
            {
                var source = await stateSet.GetByIdAsync(transition.SourceId.Value);
                if (source == null)
                    return FlowResult.Failed(new FlowError(FlowErrors.ItemNotFound, args: nameof(source)));
            }

            var destination = await stateSet.GetByIdAsync(transition.DestinationId);
            if (destination == null)
                return FlowResult.Failed(new FlowError(FlowErrors.ItemNotFound, args: nameof(destination)));

            return FlowResult.Success;
        }
    }
}
