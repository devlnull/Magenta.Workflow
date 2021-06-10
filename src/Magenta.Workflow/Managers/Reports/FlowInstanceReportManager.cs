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

        public async Task<FlowResult<FlowInstance>> GetInstanceByIdAsync(Guid id)
        {
            try
            {
                Logger.LogInfo("try to get an instance of flow by id.");
                var instanceSet = StateManager.GetFlowSet<FlowInstance>();
                var instance = await instanceSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
                if (instance == null)
                {
                    Logger.LogWarning("instance not exist.");
                    return FlowResult<FlowInstance>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowInstance)));
                }
                var result = new FlowResult<FlowInstance>();
                if (instance.Active == false)
                {
                    Logger.LogWarning("instance is inactive");
                    result.Warns.Add(new FlowWarn(FlowWarns.InstanceInactive));
                }
                result.SetResult(instance);
                Logger.LogInfo($"instance with id '{instance.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowInstance>.Failed(new FlowError(ex.Message));
            }
        }

        public async Task<FlowResult<FlowInstance>> GetInstanceAsync(
            Expression<Func<FlowInstance, bool>> expression)
        {
            try
            {
                Logger.LogInfo("try to get an instance of flow by expression.");
                var instanceSet = StateManager.GetFlowSet<FlowInstance>();
                var instance = await instanceSet.FirstOrDefaultAsync(expression);
                if (instance == null)
                {
                    Logger.LogWarning("instance not exist.");
                    return FlowResult<FlowInstance>.Failed(
                        new FlowError(FlowErrors.ItemNotfound, args: nameof(FlowInstance)));
                }
                var result = new FlowResult<FlowInstance>();
                if (instance.Active == false)
                {
                    Logger.LogWarning("instance is inactive");
                    result.Warns.Add(new FlowWarn(FlowWarns.InstanceInactive));
                }
                result.SetResult(instance);
                Logger.LogInfo($"instance with id '{instance.Id}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<FlowInstance>.Failed(new FlowError(ex.Message));
            }
        }


    }
}