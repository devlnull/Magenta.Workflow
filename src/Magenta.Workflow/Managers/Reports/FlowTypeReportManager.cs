using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<FlowResult<IEnumerable<FlowType>>> GetTypesAsync()
        {
            try
            {
                Logger.LogInfo("try to get list of types.");
                var typeSet = StateManager.GetFlowSet<FlowType>();
                var types = await typeSet.GetAllAsync();
                var result = new FlowResult<IEnumerable<FlowType>>();
                result.SetResult(types);
                Logger.LogInfo($"list of types with count '{types.Count()}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<IEnumerable<FlowType>>.Failed(new FlowError(ex.Message));
            }
        }

        public async Task<FlowResult<PagedList<FlowType>>> GetPagedTypesAsync(PageOptions pageOptions)
        {
            try
            {
                Logger.LogInfo("try to get paged list of types.");
                var typeSet = StateManager.GetFlowSet<FlowType>();
                var pagedList = await typeSet.GetPagedAllAsync(pageOptions: pageOptions);
                var result = new FlowResult<PagedList<FlowType>>();
                result.SetResult(pagedList);
                Logger.LogInfo($"paged list of types with count '{pagedList.Count}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<PagedList<FlowType>>.Failed(new FlowError(ex.Message));
            }
        }

        public async Task<FlowResult<IEnumerable<FlowType>>> GetTypesByEntityAsync(Type entityType)
        {
            try
            {
                Logger.LogInfo($"try to get list of types by entity '{entityType.FullName}'.");
                var typeSet = StateManager.GetFlowSet<FlowType>();
                var types = await typeSet
                    .GetAllAsync(x => x.EntityType.Equals(entityType.FullName));
                var result = new FlowResult<IEnumerable<FlowType>>();
                result.SetResult(types);
                Logger.LogInfo($"list of types by entity '{entityType.FullName}'" +
                               $" with count '{types.Count()}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<IEnumerable<FlowType>>.Failed(new FlowError(ex.Message));
            }
        }

        public async Task<FlowResult<PagedList<FlowType>>> GetPagedTypesByEntityAsync(Type entityType,
            PageOptions pageOptions)
        {
            try
            {
                Logger.LogInfo("try to get paged list of types.");
                var typeSet = StateManager.GetFlowSet<FlowType>();
                var pagedList = await typeSet.GetPagedAllAsync(
                    predicate: x=>x.EntityType.Equals(entityType.FullName),
                    pageOptions: pageOptions);
                var result = new FlowResult<PagedList<FlowType>>();
                result.SetResult(pagedList);
                Logger.LogInfo($"paged list of types with count '{pagedList.Count}' fetched.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return FlowResult<PagedList<FlowType>>.Failed(new FlowError(ex.Message));
            }
        }
    }
}
