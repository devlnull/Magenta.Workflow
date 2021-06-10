using System.Collections.Generic;
using Magenta.Workflow.Context.Base;

namespace Magenta.Workflow.Managers.Reports
{
    public class PagedList<TEntity> where TEntity : FlowEntity
    {
        public IEnumerable<TEntity> Items { get; set; }
        public long Count { get; set; }
    }
}
