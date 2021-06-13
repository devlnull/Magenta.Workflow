using System;
using System.Collections.Generic;
using System.Linq;
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
                        new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowState)));
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
                        new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowState)));
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

        public async Task<FlowResult<IEnumerable<FlowState>>> GetStatesByTypeIdAsync(Guid flowTypeId)
        {
            try
            {
                Logger.LogInfo($"try to get states of a flow type by id '{flowTypeId}'.");
                var stateSet = StateManager.GetFlowSet<FlowState>();
                var query = stateSet.GetAll()
                    .Where(x => x.TypeId == flowTypeId);

                var states = await stateSet.ToListAsync(query);
                var result = new FlowResult<IEnumerable<FlowState>>();
                result.SetResult(states);
                Logger.LogInfo($"list of flow states of flow type with id '{flowTypeId}' has been fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<IEnumerable<FlowState>>.Failed(new FlowError(ex.Message));
            }
        }


    }
}
