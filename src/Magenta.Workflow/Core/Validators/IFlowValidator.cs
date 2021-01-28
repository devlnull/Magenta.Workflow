using System.Threading.Tasks;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Managers.States;

namespace Magenta.Workflow.Core.Validators
{
    internal interface IFlowValidator<in TModel>
        where TModel : class
    {
        Task<FlowResult> ValidateAsync(IStateManager stateManager, TModel model);
    }
}
