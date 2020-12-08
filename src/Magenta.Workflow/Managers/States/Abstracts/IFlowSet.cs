using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magenta.Workflow.Entities.Base;

namespace Magenta.Workflow.Managers.States.Abstracts
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
        Task<TEntity> DeleteAsync(long Id);
        Task<IEnumerable<TEntity>> DeleteListAsync(IEnumerable<long> Ids);
        Task<IEnumerable<TEntity>> DeleteListAsync(IEnumerable<Guid> guids);
        Task<IEnumerable<TEntity>> PhysicalDeleteListAsync(IEnumerable<long> Ids);
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
        Task<long> GetIdByGuidAsync(Guid Id);
        Task<TEntity> GetByIdAsync(long Id);
        Task<TEntity> GetByGuidAsync(Guid Id);
        IQueryable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
}
