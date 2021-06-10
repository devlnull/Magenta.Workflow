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
    public class InitFlowTransitionValidator: IFlowValidator<InitFlowTransitionModel>
    {
        public async Task<FlowResult> ValidateAsync(IStateManager stateManager, InitFlowTransitionModel model)
        {
            FlowResult result = new FlowResult();

            if (model.Name.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsrequired, nameof(model.Name)));

            if (model.Title.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsrequired, nameof(model.Title)));

            if (await StateNotExistAsync(stateManager, model.SourceId))
                result.Errors.Add(new FlowError(FlowErrors.ItemNotfound, "Source"));

            if (await StateNotExistAsync(stateManager, model.DestinationId))
                result.Errors.Add(new FlowError(FlowErrors.ItemNotfound, "Destination"));

            if (await TransitionInPathAlreadyExistAsync(stateManager, model.SourceId, model.DestinationId))
                result.Warns.Add(new FlowWarn(FlowMessages.TransitionInpathexist));

            return result;
        }

        private async Task<bool> StateNotExistAsync(IStateManager stateManager, Guid stateGuidRow)
        {
            var transitionSet = stateManager.GetFlowSet<FlowState>();
            var item = await transitionSet.GetByIdAsync(stateGuidRow);
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
