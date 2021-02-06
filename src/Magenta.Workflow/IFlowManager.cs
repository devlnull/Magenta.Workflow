using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.UseCases.InitFlow;
using Magenta.Workflow.UseCases.InitFlowState;
using Magenta.Workflow.UseCases.InitFlowTransition;
using Magenta.Workflow.UseCases.InitFlowType;

namespace Magenta.Workflow
{
    public interface IFlowManager
    {
        #region Init

        /// <summary>
        /// Initialize new flow
        /// </summary>
        Task<FlowResult<FlowInstance>> InitFlowAsync(InitFlowModel initModel);

        /// <summary>
        /// Initialize new flow type
        /// </summary>
        Task<FlowResult<FlowType>> InitFlowTypeAsync(InitFlowTypeModel initModel);

        /// <summary>
        /// Initialize new flow state
        /// </summary>
        Task<FlowResult<FlowState>> InitFlowStateAsync(InitFlowStateModel initModel);

        /// <summary>
        /// Initialize new transition
        /// </summary>
        Task<FlowResult<FlowTransition>> InitFlowTransitionAsync(InitFlowTransitionModel initModel);

        #endregion Init
    }
}