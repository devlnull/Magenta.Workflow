using System;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Core.Validators;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.UseCases.InitFlowState
{
    public class InitFlowStateValidator : IFlowValidator<InitFlowStateModel>
    {
        public async Task<FlowResult> ValidateAsync(IStateManager stateManager, InitFlowStateModel model)
        {
            FlowResult result = new FlowResult();

            if (model.Name.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsrequired, args: nameof(model.Name)));

            if (model.Title.StringIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsrequired, args: nameof(model.Title)));
            
            if (model.TypeId.GuidIsEmpty())
                result.Errors.Add(new FlowError(FlowErrors.ServiceIsrequired, args: nameof(model.TypeId)));

            if (await TypeNotExistAsync(stateManager, model.TypeId))
                result.Errors.Add(new FlowError(FlowErrors.ItemNotfound, args: "Type"));


            return result;
        }

        private async Task<bool> TypeNotExistAsync(IStateManager stateManager, Guid typeGuidRow)
        {
            var typeSet = stateManager.GetFlowSet<FlowType>();
            var item = await typeSet.GetByIdAsync(typeGuidRow);
            return item == null;
        }
    }
}
