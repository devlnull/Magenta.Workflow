using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Flows;
using Magenta.Workflow.Core.Tasks;
using Magenta.Workflow.Utilities;

namespace Magenta.Workflow.Managers.Reports
{
    public partial class FlowReportManager
    {

        public async Task<FlowResult<FlowState>> GetStateByIdAsync(Guid id)
        {
            try
            {
                Logger.LogInfo("try to get a state of flow by id.");
                var stateSet = StateManager.GetFlowSet<FlowState>();
                var state = await stateSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (state == null)
                {
                    Logger.LogWarning("state not exist.");
                    return FlowResult<FlowState>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowState)));
                }
                var result = new FlowResult<FlowState>();
                result.SetResult(state);
                Logger.LogInfo($"state with id '{state.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowState>.Failed(new FlowError(ex.Message));
            }
        }

        public async Task<FlowResult<FlowState>> GetStateAsync(
            Expression<Func<FlowState, bool>> expression)
        {
            try
            {
                Logger.LogInfo("try to get a state of flow by expression.");
                var stateSet = StateManager.GetFlowSet<FlowState>();
                var state = await stateSet.FirstOrDefaultAsync(expression);
                if (state == null)
                {
                    Logger.LogWarning("state not exist.");
                    return FlowResult<FlowState>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowState)));
                }
                var result = new FlowResult<FlowState>();
                result.SetResult(state);
                Logger.LogInfo($"state with id '{state.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowState>.Failed(new FlowError(ex.Message));
            }
        }


    }
}
