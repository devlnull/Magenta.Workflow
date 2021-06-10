using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Core.Exceptions;
using Magenta.Workflow.Managers.Reports;

namespace Magenta.Workflow.Managers.States
{
    public class InMemoryFlowSet<TEntity> : IFlowSet<TEntity>
        where TEntity : FlowEntity
    {
        private List<TEntity> _set;
        public IEnumerable<TEntity> DataSet
        {
            get
            {
                _set ??= new List<TEntity>();
                return _set.AsEnumerable();
            }
            set => _set = value.ToList();
        }

        public string EntityName => typeof(TEntity).Name;

        #region Helpers

        private PageOptions ResolvePageOptions(PageOptions pageOptions)
        {
            if (pageOptions.Limit.HasValue == false)
                pageOptions.Limit = 10;
            if (pageOptions.Offset.HasValue == false)
                pageOptions.Offset = 0;
            return pageOptions;
        }

        #endregion Helpers

        public InMemoryFlowSet()
        {
            _set ??= new List<TEntity>();
        }

        #region Utilities

        public Task<bool> AnyAsync()
        {
            return Task.FromResult(_set.Any());
        }

        public Task<long> CountAsync()
        {
            return Task.FromResult(_set.LongCount());
        }

        #endregion Utilities

        #region Create

        public Task<TEntity> CreateAsync(TEntity input)
        {
            _set.Add(input);
            return Task.FromResult(input);
        }

        public Task<IEnumerable<TEntity>> CreateListAsync(IEnumerable<TEntity> input)
        {
            if (input == null)
                return null;
            if (!input.Any())
                return null;

            foreach (var item in input)
                _set.Add(item);
            return Task.FromResult(input);
        }

        #endregion Create

        #region Delete

        public Task<TEntity> DeleteAsync(Guid id)
        {
            var item = _set.FirstOrDefault(x => x.Id.Equals(id));
            if (item == null)
                throw new FlowException($"Could not find item with this identifier.");
            item.Deleted = true;
            UpdateAsync(item);
            return Task.FromResult(item);
        }

        public Task<IEnumerable<TEntity>> DeleteListAsync(IEnumerable<Guid> ids)
        {
            var items = _set.Where(x => ids.Contains(x.Id));
            if (items == null)
                throw new FlowException($"Could not find any item with this identifiers.");

            foreach (var item in items)
            {
                item.Deleted = true;
                UpdateAsync(item);
            }

            return Task.FromResult(items);
        }

        public Task<IEnumerable<TEntity>> PhysicalDeleteListAsync(IEnumerable<Guid> ids)
        {
            var items = _set.Where(x => ids.Contains(x.Id));
            if (items == null)
                throw new FlowException($"Could not find any item with this identifiers.");
            _set.RemoveAll(x => ids.Contains(x.Id));
            return Task.FromResult(items);
        }

        #endregion Delete

        #region Get

        public Task<IEnumerable<TEntity>> ToListAsync(IQueryable<TEntity> query)
        {
            var result = query.ToList();
            return Task.FromResult<IEnumerable<TEntity>>(query);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var item = _set.FirstOrDefault(predicate.Compile());
            return Task.FromResult(item);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _set.AsQueryable();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IEnumerable<TEntity> items = null;
            if (predicate == null)
                items = _set;
            else
                items = _set.Where(predicate.Compile());

            return Task.FromResult(items);
        }

        public Task<PagedList<TEntity>> GetPagedAllAsync(PageOptions pageOptions,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            IEnumerable<TEntity> items = null;
            if (predicate == null)
                items = _set;
            else
                items = _set.Where(predicate.Compile());

            pageOptions = ResolvePageOptions(pageOptions);

            items = _set
                .Skip(pageOptions.GetOffset().Value)
                .Take(pageOptions.GetLimit().Value)
                .ToList();

            var pagedList = new PagedList<TEntity>()
            {
                Items = items,
                Count = _set.Count()
            };

            return Task.FromResult(pagedList);
        }

        public Task<TEntity> GetByIdAsync(Guid id)
        {
            var item = _set.FirstOrDefault(x => x.Id.Equals(id));
            return Task.FromResult(item);
        }

        #endregion Get

        #region Update

        public Task<TEntity> UpdateAsync(TEntity input)
        {
            var item = _set.FirstOrDefault(x => x.Id.Equals(input.Id));

            item.ModifiedAt = DateTime.Now;
            _set.Remove(input);
            _set.Add(input);
            return Task.FromResult(input);
        }

        public Task<IEnumerable<TEntity>> UpdateListAsync(IEnumerable<TEntity> input)
        {
            foreach (var item in input)
                UpdateAsync(item);
            return Task.FromResult(input);
        }

        #endregion Update
    }
}
