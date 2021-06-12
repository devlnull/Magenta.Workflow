using Magenta.Workflow.Context.Flows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magenta.Workflow.SqlServer.Integrations.EntityConfigurations
{
    public class FlowTransitionReasonConfiguration :IEntityTypeConfiguration<FlowTransitionReason>
    {
        public void Configure(EntityTypeBuilder<FlowTransitionReason> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasOne(x => x.Transition)
                .WithMany(x => x.Reasons)
                .HasForeignKey(x => x.TransitionId);
        }
    }
}
