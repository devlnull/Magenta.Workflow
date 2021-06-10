using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magenta.Workflow.Managers.Reports;

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
        Task<FlowResult<FlowInstance>> GetInstanceByIdAsync(Guid id);

        /// <summary>
        /// Get an instance by expression
        /// </summary>
        /// <returns>a flow instance</returns>
        Task<FlowResult<FlowInstance>> GetInstanceAsync(
            Expression<Func<FlowInstance, bool>> expression);

        #endregion Instance Reports

        #region State Reports

        /// <summary>
        /// Get state of flow by state id
        /// </summary>
        /// <returns>a flow state</returns>
        Task<FlowResult<FlowState>> GetStateByIdAsync(Guid id);

        /// <summary>
        /// Get state of flow by expression
        /// </summary>
        /// <returns>a flow state</returns>
        Task<FlowResult<FlowState>> GetStateAsync(
            Expression<Func<FlowState, bool>> expression);

        #endregion State Reports

        #region Identity Reports
        #endregion Identity Reports

        #region Type Reports

        /// <summary>
        /// Get type of flow by type id
        /// </summary>
        /// <returns>a flow type</returns>
        Task<FlowResult<FlowType>> GetTypeByIdAsync(Guid id);

        /// <summary>
        /// Get type of flow by expression
        /// </summary>
        /// <returns>a flow type</returns>
        Task<FlowResult<FlowType>> GetTypeAsync(
            Expression<Func<FlowType, bool>> expression);

        /// <summary>
        /// Get list of all flow types
        /// </summary>
        /// <returns>list of flow types</returns>
        Task<FlowResult<IEnumerable<FlowType>>> GetTypesAsync();

        /// <summary>
        /// Get paged list of all flow types
        /// </summary>
        /// <returns>paged list of flow types</returns>
        Task<FlowResult<PagedList<FlowType>>> GetPagedTypesAsync(PageOptions pageOptions);

        /// <summary>
        /// Get list of all flow types by entity type
        /// </summary>
        /// <returns>list of flow types</returns>
        Task<FlowResult<IEnumerable<FlowType>>> GetTypesByEntityAsync(Type entityType);

        /// <summary>
        /// Get paged list of all flow types by entity type
        /// </summary>
        /// <returns>paged list of flow types</returns>
        Task<FlowResult<PagedList<FlowType>>> GetPagedTypesByEntityAsync(Type entityType,
            PageOptions pageOptions);

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
