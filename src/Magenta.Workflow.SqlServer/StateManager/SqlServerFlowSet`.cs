using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Managers.Reports;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.SqlServer.Integrations;
using Microsoft.EntityFrameworkCore;

namespace Magenta.Workflow.SqlServer.StateManager
{
    public class SqlServerFlowSet<TEntity> : IFlowSet<TEntity> where TEntity : FlowEntity
    {
        private readonly DbSet<TEntity> _set;
        private readonly WorkflowDbContext _context;

        public SqlServerFlowSet(WorkflowDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> DataSet { get; set; }

        private PageOptions ResolvePageOptions(PageOptions pageOptions)
        {
            if (pageOptions.Limit.HasValue == false)
                pageOptions.Limit = 10;
            if (pageOptions.Offset.HasValue == false)
                pageOptions.Offset = 0;
            return pageOptions;
        }

        public async Task<long> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _set.CountAsync(x => x.Deleted == false,
                cancellationToken: cancellationToken);
        }

        public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await _set.AnyAsync(x => x.Deleted == false,
                cancellationToken: cancellationToken);
        }

        public async Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var item = await GetByIdAsync(id, cancellationToken);
            item.Deleted = true;
            await UpdateAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }

        public async Task<IEnumerable<TEntity>> DeleteListAsync(IEnumerable<Guid> guids,
            CancellationToken cancellationToken = default)
        {
            var items = await GetAllAsync(x => guids.Contains(x.Id),
                cancellationToken);
            foreach (var item in items)
                item.Deleted = true;

            await UpdateListAsync(items, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return items;
        }

        public async Task<IEnumerable<TEntity>> PhysicalDeleteListAsync(IEnumerable<Guid> guids,
            CancellationToken cancellationToken = default)
        {
            var items = await GetAllAsync(x => guids.Contains(x.Id),
                cancellationToken);
            foreach (var item in items)
                _set.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return items;
        }

        public async Task<TEntity> UpdateAsync(TEntity input, CancellationToken cancellationToken = default)
        {
            var targetItem = await _set
                .FirstOrDefaultAsync(x => x.Id.Equals(input.Id), cancellationToken);
            input.ModifiedAt = DateTime.Now;
            _context.Entry(targetItem).CurrentValues.SetValues(input);
            await _context.SaveChangesAsync(cancellationToken);
            return targetItem;
        }

        public async Task<IEnumerable<TEntity>> UpdateListAsync(IEnumerable<TEntity> input,
            CancellationToken cancellationToken = default)
        {
            List<TEntity> items = new List<TEntity>(input.Count());
            foreach (var item in input)
                items.Add(await UpdateAsync(item, cancellationToken));
            return items;
        }

        public async Task<TEntity> CreateAsync(TEntity input, CancellationToken cancellationToken = default)
        {
            await _set.AddAsync(input, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return input;
        }

        public async Task<IEnumerable<TEntity>> CreateListAsync(IEnumerable<TEntity> input,
            CancellationToken cancellationToken = default)
        {
            await _set.AddRangeAsync(input, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return input;
        }

        public async Task<IEnumerable<TEntity>> ToListAsync(IQueryable<TEntity> query,
            CancellationToken cancellationToken = default)
        {
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<PagedList<TEntity>> ToPagedListAsync(IQueryable<TEntity> query,
            PageOptions pageOptions, CancellationToken cancellationToken = default)
        {
            pageOptions = ResolvePageOptions(pageOptions);

            var result = await query
                .Skip(pageOptions.GetOffset().Value)
                .Take(pageOptions.GetLimit().Value)
                .ToListAsync(cancellationToken);

            return new PagedList<TEntity>()
            {
                Items = result,
                Count = await query.CountAsync(cancellationToken)
            };
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var item = await _set
                .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
            return item;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _set;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            CancellationToken cancellationToken = default)
        {
            var query = _set.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<PagedList<TEntity>> GetPagedAllAsync(PageOptions pageOptions,
            Expression<Func<TEntity, bool>> predicate = null,
            CancellationToken cancellationToken = default)
        {
            pageOptions = ResolvePageOptions(pageOptions);

            var query = _set.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);

            var result = await query
                .Skip(pageOptions.GetOffset().Value)
                .Take(pageOptions.GetLimit().Value)
                .ToListAsync(cancellationToken);

            return new PagedList<TEntity>()
            {
                Items = result,
                Count = await query.CountAsync(cancellationToken)
            };
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _set.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync(IQueryable<TEntity> query,
            CancellationToken cancellationToken = default)
        {
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
