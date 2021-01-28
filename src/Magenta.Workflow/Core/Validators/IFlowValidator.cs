using System.Threading.Tasks;
using Magenta.Workflow.Core.Tasks;

namespace Magenta.Workflow.Core.Validators
{
    internal interface IFlowValidator<in TModel>
        where TModel : class
    {
        Task<FlowResult> ValidateAsync(TModel model);
    }
}
