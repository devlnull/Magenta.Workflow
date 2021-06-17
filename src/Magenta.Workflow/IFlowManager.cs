using System.Threading.Tasks;
using Magenta.Workflow.Builder;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.UseCases.InitFlow;
using Magenta.Workflow.UseCases.InitFlowState;
using Magenta.Workflow.UseCases.InitFlowTransition;
using Magenta.Workflow.UseCases.InitFlowType;
using Magenta.Workflow.UseCases.Move;

namespace Magenta.Workflow
{
    public interface IFlowManager
    {
        #region Init

        /// <summary>
        /// Initialize new flow
        /// </summary>
        Task<FlowResult<FlowInstance>> InitFlowAsync(InitFlowRequest initRequest);

        /// <summary>
        /// Initialize new flow type
        /// </summary>
        Task<FlowResult<FlowType>> InitFlowTypeAsync(InitFlowTypeRequest initRequest);

        /// <summary>
        /// Initialize new flow state
        /// </summary>
        Task<FlowResult<FlowState>> InitFlowStateAsync(InitFlowStateRequest initRequest);

        /// <summary>
        /// Initialize new transition
        /// </summary>
        Task<FlowResult<FlowTransition>> InitFlowTransitionAsync(InitFlowTransitionRequest initRequest);

        #endregion Init

        #region Move
        /// <summary>
        /// Move instance to next state
        /// </summary>
        Task<FlowResult<FlowStep>> MoveAsync(MoveRequest moveRequest);

        #endregion Move

        #region Build
        /// <summary>
        /// workflow builder get a configuration by builder class and makes a flow type with all details
        /// </summary>
        /// <returns>build result</returns>
        Task<FlowResult> BuildWorkflowAsync(WorkflowBuilder workflowBuilder);

        #endregion Build
    }
}