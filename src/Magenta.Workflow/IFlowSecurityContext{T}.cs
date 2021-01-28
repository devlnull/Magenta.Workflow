using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenta.Workflow.Managers.Flows;

namespace Magenta.Workflow
{
    public interface IFlowSecurityContext<TKey>
    where TKey : IComparable
    {
        #region Get
        #region User
        TKey GetUserId();
        Task<TKey> GetUserIdAsync();
        Task<TKey> GetAutomationUserIdAsync();
        Task<Dictionary<string, string>> GetUsernameAndIdByIdAsync(IEnumerable<string> userIds);
        #endregion
        #region Role
        Task<IEnumerable<TKey>> GetUserRoleIdsAsync();
        #endregion
        #endregion

        #region Filter Access Cartable
        Task<IQueryable<FlowCartableModel>> FilterAccessCartableAsync(IQueryable<FlowCartableModel> query);
        #endregion
    }
}
