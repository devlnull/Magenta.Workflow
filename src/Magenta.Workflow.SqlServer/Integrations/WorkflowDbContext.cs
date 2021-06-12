using Magenta.Workflow.Context.Flows;
using Microsoft.EntityFrameworkCore;

namespace Magenta.Workflow.SqlServer.Integrations
{
    public class WorkflowDbContext : DbContext
    {
        public WorkflowDbContext()
        {
        }

        public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options) : base(options)
        {

        }

        public DbSet<FlowType> FlowTypes { get; set; }
        public DbSet<FlowInstance> FlowInstances { get; set; }
        public DbSet<FlowState> FlowStates { get; set; }
        public DbSet<FlowStep> FlowSteps { get; set; }
        public DbSet<FlowIdentity> FlowIdentities { get; set; }
        public DbSet<FlowTransition> FlowTransitions { get; set; }
        public DbSet<FlowTransitionReason> FlowTransitionReasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
