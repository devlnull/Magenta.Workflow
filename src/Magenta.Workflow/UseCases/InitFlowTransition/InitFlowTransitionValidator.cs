using System;
using System.Linq;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Core.Validators;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.UseCases.InitFlowTransition
{
    public class InitFlowTransitionValidator: IFlowValidator<InitFlowTransitionRequest>
    {
        public async Task<FlowResult> ValidateAsync(IStateManager stateManager, InitFlowTransitionRequest request)
        {
            FlowResult result = new FlowResult();

            if (request.Name.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsRequired, nameof(request.Name)));

            if (request.Title.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsRequired, nameof(request.Title)));

            if (await StateNotExistAsync(stateManager, request.SourceId))
                result.Errors.Add(new FlowError(FlowErrors.ItemNotFound, "Source"));

            if (await StateNotExistAsync(stateManager, request.DestinationId))
                result.Errors.Add(new FlowError(FlowErrors.ItemNotFound, "Destination"));

            if (await TypeNotExistAsync(stateManager, request.TypeId))
                result.Errors.Add(new FlowError(FlowErrors.ItemNotFound, "Type"));

            if (await TransitionInPathAlreadyExistAsync(stateManager, request.SourceId, request.DestinationId))
                result.Warns.Add(new FlowWarn(FlowMessages.TransitionInpathexist));

            return result;
        }

        private async Task<bool> StateNotExistAsync(IStateManager stateManager, Guid stateId)
        {
            var stateSet = stateManager.GetFlowSet<FlowState>();
            var item = await stateSet.GetByIdAsync(stateId);
            return item == null;
        }

        private async Task<bool> TypeNotExistAsync(IStateManager stateManager, Guid typeId)
        {
            var typeSet = stateManager.GetFlowSet<FlowType>();
            var item = await typeSet.GetByIdAsync(typeId);
            return item == null;
        }

        private async Task<bool> TransitionInPathAlreadyExistAsync(IStateManager stateManager, 
            Guid sourceGuid, Guid destinationGuid)
        {
            var stateSet = stateManager.GetFlowSet<FlowState>();
            var transitionSet = stateManager.GetFlowSet<FlowTransition>();

            var source = await stateSet.GetByIdAsync(sourceGuid);
            var destination = await stateSet.GetByIdAsync(destinationGuid);

            if (source == null || destination == null)
                return false;

            var items = await transitionSet
                .GetAllAsync(x => x.SourceId == source.Id && x.DestinationId == destination.Id);

            return items.Any();
        }
    }
}
