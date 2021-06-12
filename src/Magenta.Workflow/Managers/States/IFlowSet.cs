using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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
        Task<long> CountAsync(CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);
        #endregion

        #region Delete
        Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> DeleteListAsync(IEnumerable<Guid> guids,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> PhysicalDeleteListAsync(IEnumerable<Guid> guids,
            CancellationToken cancellationToken = default);
        #endregion

        #region Update
        Task<TEntity> UpdateAsync(TEntity input, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> UpdateListAsync(IEnumerable<TEntity> input, CancellationToken cancellationToken = default);
        #endregion

        #region Create
        Task<TEntity> CreateAsync(TEntity input, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> CreateListAsync(IEnumerable<TEntity> input, CancellationToken cancellationToken = default);
        #endregion

        #region Get

        Task<IEnumerable<TEntity>> ToListAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
        Task<PagedList<TEntity>> ToPagedListAsync(IQueryable<TEntity> query, PageOptions pageOptions,
            CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        IQueryable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, 
            CancellationToken cancellationToken = default);
        Task<PagedList<TEntity>> GetPagedAllAsync(PageOptions pageOptions,
            Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity> FirstOrDefaultAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
        #endregion
    }
}
