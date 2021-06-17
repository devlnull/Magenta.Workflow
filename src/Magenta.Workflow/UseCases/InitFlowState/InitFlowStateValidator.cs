using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Core.Validators;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.UseCases.InitFlowState
{
    public class InitFlowStateValidator : IFlowValidator<InitFlowStateRequest>
    {
        public async Task<FlowResult> ValidateAsync(IStateManager stateManager, InitFlowStateRequest request)
        {
            FlowResult result = new FlowResult();

            if (request.Name.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsRequired, args: nameof(request.Name)));

            if (request.Title.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsRequired, args: nameof(request.Title)));

            if (request.Tag.StringIsEmpty())
                result.Warns.Add(new FlowWarn(FlowErrors.ServiceIsEmpty, args: nameof(request.Tag)));

            if (request.TypeId.GuidIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsRequired, args: nameof(request.TypeId)));

            if (await TypeNotExistAsync(stateManager, request.TypeId))
                result.Errors.Add(new FlowError(FlowErrors.ItemNotFound, args: "Type"));


            return result;
        }

        private async Task<bool> TypeNotExistAsync(IStateManager stateManager, Guid typeId)
        {
            var typeSet = stateManager.GetFlowSet<FlowType>();
            var item = await typeSet.GetByIdAsync(typeId);
            return item == null;
        }
    }
}
