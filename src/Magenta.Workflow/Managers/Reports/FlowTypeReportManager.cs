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
        public async Task<FlowResult<FlowType>> GetTypeByIdAsync(Guid id)
        {
            try
            {
                Logger.LogInfo("try to get a type of flow by id.");
                var typeSet = StateManager.GetFlowSet<FlowType>();
                var stateSet = StateManager.GetFlowSet<FlowState>();
                var query = from type in typeSet.GetAll()
                            join state in stateSet.GetAll() on type.Id equals state.TypeId into states
                            where type.Id.Equals(id)
                            select new FlowType()
                            {
                                CreatedAt = type.CreatedAt,
                                Deleted = type.Deleted,
                                Id = type.Id,
                                ModifiedAt = type.ModifiedAt,
                                Name = type.Name,
                                States = states.ToList()
                            };
                var flowType = await typeSet.FirstOrDefaultAsync(query);

                if (flowType == null)
                {
                    Logger.LogWarning("type not exist.");
                    return FlowResult<FlowType>.Failed(
                        new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowType)));
                }
                var result = new FlowResult<FlowType>();
                result.SetResult(flowType);
                Logger.LogInfo($"type with id '{flowType.Id}' fetched.");
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
                var stateSet = StateManager.GetFlowSet<FlowState>();

                var query = from type in typeSet.GetAll()
                            let states = stateSet.GetAll().Where(x => x.TypeId == type.Id)
                            select new FlowType()
                            {
                                CreatedAt = type.CreatedAt,
                                Deleted = type.Deleted,
                                Id = type.Id,
                                ModifiedAt = type.ModifiedAt,
                                Name = type.Name,
                                States = states.ToList()
                            };

                query = query.Where(expression);

                var flowType = await typeSet.FirstOrDefaultAsync(query);

                if (flowType == null)
                {
                    Logger.LogWarning("type not exist.");
                    return FlowResult<FlowType>.Failed(
                        new FlowError(FlowErrors.ItemNotFound, args: nameof(FlowType)));
                }
                var result = new FlowResult<FlowType>();
                result.SetResult(flowType);
                Logger.LogInfo($"type with id '{flowType.Id}' fetched.");
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
                var stateSet = StateManager.GetFlowSet<FlowState>();

                var query = from type in typeSet.GetAll()
                            let states = stateSet.GetAll().Where(x => x.TypeId == type.Id)
                            select new FlowType()
                            {
                                CreatedAt = type.CreatedAt,
                                Deleted = type.Deleted,
                                Id = type.Id,
                                ModifiedAt = type.ModifiedAt,
                                Name = type.Name,
                                States = states.ToList()
                            };

                var types = await typeSet.ToListAsync(query);

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
                var stateSet = StateManager.GetFlowSet<FlowState>();
                var query = from type in typeSet.GetAll()
                            let states = stateSet.GetAll().Where(x => x.TypeId == type.Id)
                            select new FlowType()
                            {
                                CreatedAt = type.CreatedAt,
                                Deleted = type.Deleted,
                                Id = type.Id,
                                ModifiedAt = type.ModifiedAt,
                                Name = type.Name,
                                States = states.ToList()
                            };
                var pagedList = await typeSet.ToPagedListAsync(query, pageOptions);
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
