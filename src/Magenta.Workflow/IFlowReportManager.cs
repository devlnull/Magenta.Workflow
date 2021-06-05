using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Magenta.Workflow
{
    public interface IFlowReportManager
    {
        #region Transitions
        /// <summary>
        /// Get instance current transitions based on current instance state
        /// </summary>
        /// <returns>all possible transitions</returns>
        Task<FlowResult<IEnumerable<FlowTransition>>> GetInstanceTransitionsAsync(Guid instanceId);
        #endregion Transitions

        #region Steps
        /// <summary>
        /// Get list of all steps that an instance has taken
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns>list of steps</returns>
        Task<FlowResult<IEnumerable<FlowStep>>> GetInstanceStepsAsync(Guid instanceId);
        #endregion Steps

        #region Instance Reports

        /// <summary>
        /// Get an instance by instance id
        /// </summary>
        /// <returns>a flow instance</returns>
        Task<FlowResult<FlowInstance>> GetInstanceByIdAsync(Guid Id);

        /// <summary>
        /// Get an instance by expression
        /// </summary>
        /// <returns>a flow instance</returns>
        Task<FlowResult<FlowInstance>> GetInstanceAsync(
            Expression<Func<FlowInstance, bool>> expression);

        #endregion Instance Reports


        #region Identity Reports
        #endregion Identity Reports

        #region Type Reports

        /// <summary>
        /// Get type of flow by type id
        /// </summary>
        /// <returns>a flow type</returns>
        Task<FlowResult<FlowType>> GetTypeByIdAsync(Guid Id);

        /// <summary>
        /// Get type of flow by expression
        /// </summary>
        /// <returns>a flow type</returns>
        Task<FlowResult<FlowType>> GetTypeAsync(
            Expression<Func<FlowType, bool>> expression);

        #endregion Type Reports

        #region Overview Reports

        // Number of instances, types, users, roles(groups)
        //Current status of flow

        #endregion Overview Reports

        #region Complex Reports

        // Flow history

        #endregion Complex Reports
    }
}
