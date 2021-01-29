using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.UseCases.InitFlow;
using Magenta.Workflow.UseCases.InitFlowType;

namespace Magenta.Workflow
{
    public interface IFlowManager
    {
        #region Init

        Task<FlowResult<FlowInstance>> InitFlowAsync(InitFlowModel initModel);
        Task<FlowResult<FlowType>> InitFlowTypeAsync(InitFlowTypeModel initModel);

        #endregion Init
    }
}