using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Managers.Reports;

namespace Magenta.Workflow.Managers.States
{
    public interface IFlowSet<TEntity>
    where TEntity : FlowEntity
    {
        IEnumerable<TEntity> DataSet { get; set; }

        #region Utility
        Task<long> CountAsync();
        Task<bool> AnyAsync();
        #endregion

        #region Delete
        Task<TEntity> DeleteAsync(Guid id);
        Task<IEnumerable<TEntity>> DeleteListAsync(IEnumerable<Guid> guids);
        Task<IEnumerable<TEntity>> PhysicalDeleteListAsync(IEnumerable<Guid> guids);
        #endregion

        #region Update
        Task<TEntity> UpdateAsync(TEntity input);
        Task<IEnumerable<TEntity>> UpdateListAsync(IEnumerable<TEntity> input);
        #endregion

        #region Create
        Task<TEntity> CreateAsync(TEntity input);
        Task<IEnumerable<TEntity>> CreateListAsync(IEnumerable<TEntity> input);
        #endregion

        #region Get
        Task<TEntity> GetByIdAsync(Guid id);
        IQueryable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<PagedList<TEntity>> GetPagedAllAsync(PageOptions pageOptions,
            Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
}
