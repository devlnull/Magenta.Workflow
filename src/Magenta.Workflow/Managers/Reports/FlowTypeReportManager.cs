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

        public async Task<FlowResult<FlowType>> GetTypeByIdAsync(Guid id)
        {
            try
            {
                Logger.LogInfo("try to get a type of flow by id.");
                var typeSet = StateManager.GetFlowSet<FlowType>();
                var type = await typeSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (type == null)
                {
                    Logger.LogWarning("type not exist.");
                    return FlowResult<FlowType>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowType)));
                }
                var result = new FlowResult<FlowType>();
                result.SetResult(type);
                Logger.LogInfo($"type with id '{type.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowType>.Failed(new FlowError(ex.Message));
            }
        }

        public async Task<FlowResult<FlowType>> GetTypeAsync(
            Expression<Func<FlowType, bool>> expression)
        {
            try
            {
                Logger.LogInfo("try to get a type of flow by expression.");
                var typeSet = StateManager.GetFlowSet<FlowType>();
                var type = await typeSet.FirstOrDefaultAsync(expression);
                if (type == null)
                {
                    Logger.LogWarning("type not exist.");
                    return FlowResult<FlowType>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowType)));
                }
                var result = new FlowResult<FlowType>();
                result.SetResult(type);
                Logger.LogInfo($"type with id '{type.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowType>.Failed(new FlowError(ex.Message));
            }
        }

    }
}
