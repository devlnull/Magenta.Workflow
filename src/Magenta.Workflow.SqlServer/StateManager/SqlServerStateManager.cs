using System;
using Magenta.Workflow.Context.Base;
using Magenta.Workflow.Managers.States;
using Magenta.Workflow.SqlServer.Integrations;

namespace Magenta.Workflow.SqlServer.StateManager
{
    public class SqlServerStateManager : IStateManager
    {
        private readonly WorkflowDbContext _context;

        public SqlServerStateManager(WorkflowDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public IFlowSet<TEntity> GetFlowSet<TEntity>() where TEntity : FlowEntity
        {
            var set = new SqlServerFlowSet<TEntity>(_context);
            return set;
        }
    }
}
