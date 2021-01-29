using System.Threading.Tasks;
using Magenta.Workflow.Core.Tasks;

namespace Magenta.Workflow.UseCases
{
    internal interface IFlowRequest<in TModel, TResultModel>
        where TModel : class where TResultModel : class
    {
        Task<FlowResult<TResultModel>> DoAsync(TModel model);
    }
}
