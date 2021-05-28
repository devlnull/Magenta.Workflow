using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Core.Exceptions;

namespace Magenta.Workflow.Managers.States
{
    public class InMemoryFlowSet<TEntity> : IFlowSet<TEntity>
        where TEntity : FlowEntity
    {
        private List<TEntity> _repo;

        public IEnumerable<TEntity> DataSet
        {
            get
            {
                if (_repo == null)
                    _repo = new List<TEntity>();
                return _repo.AsEnumerable();
            }
            set => _repo = value.ToList();
        }

        public string EntityName => typeof(TEntity).Name;

        public InMemoryFlowSet()
        {
            if(_repo == null)
                _repo = new List<TEntity>();
        }
        
        #region Utilities

        public Task<bool> AnyAsync()
        {
            return Task.FromResult(_repo.Any());
        }

        public Task<long> CountAsync()
        {
            return Task.FromResult(_repo.LongCount());
        }

        #endregion Utilities

        #region Create

        public Task<TEntity> CreateAsync(TEntity input)
        {
            _repo.Add(input);
            return Task.FromResult(input);
        }

        public Task<IEnumerable<TEntity>> CreateListAsync(IEnumerable<TEntity> input)
        {
            if (input == null)
                return null;
            if (!input.Any())
                return null;

            foreach (var item in input)
                _repo.Add(item);
            return Task.FromResult(input);
        }

        #endregion Create

        #region Delete

        public Task<TEntity> DeleteAsync(Guid Id)
        {
            var item = _repo.FirstOrDefault(x => x.Id.Equals(Id));
            if (item == null)
                throw new FlowException($"Could not find item with this identifier.");
            item.Deleted = true;
            UpdateAsync(item);
            return Task.FromResult(item);
        }

        public Task<IEnumerable<TEntity>> DeleteListAsync(IEnumerable<Guid> Ids)
        {
            var items = _repo.Where(x => Ids.Contains(x.Id));
            if (items == null)
                throw new FlowException($"Could not find any item with this identifiers.");

            foreach (var item in items)
            {
                item.Deleted = true;
                UpdateAsync(item);
            }

            return Task.FromResult(items);
        }

        public Task<IEnumerable<TEntity>> PhysicalDeleteListAsync(IEnumerable<Guid> Ids)
        {
            var items = _repo.Where(x => Ids.Contains(x.Id));
            if (items == null)
                throw new FlowException($"Could not find any item with this identifiers.");
            _repo.RemoveAll(x => Ids.Contains(x.Id));
            return Task.FromResult(items);
        }

        #endregion Delete

        #region Get

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var item = _repo.FirstOrDefault(predicate.Compile());
            return Task.FromResult(item);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _repo.AsQueryable();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IEnumerable<TEntity> items = null;
            if (predicate == null)
                items = _repo;
            else
                items = _repo.Where(predicate.Compile());

            return Task.FromResult(items);
        }

        public Task<TEntity> GetByIdAsync(Guid Id)
        {
            var item = _repo.FirstOrDefault(x => x.Id.Equals(Id));
            return Task.FromResult(item);
        }

        #endregion Get

        #region Update

        public Task<TEntity> UpdateAsync(TEntity input)
        {
            var item = _repo.FirstOrDefault(x => x.Id.Equals(input.Id));

            item.ModifiedAt = DateTime.Now;
            _repo.Remove(input);
            _repo.Add(input);
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
