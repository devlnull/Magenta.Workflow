using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using System;
using System.Collections.Generic;
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
    }
}
