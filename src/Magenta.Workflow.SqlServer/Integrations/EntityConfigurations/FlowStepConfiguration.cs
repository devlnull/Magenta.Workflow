using Magenta.Workflow.Context.Flows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magenta.Workflow.SqlServer.Integrations.EntityConfigurations
{
    public class FlowStepConfiguration : IEntityTypeConfiguration<FlowStep>
    {
        public void Configure(EntityTypeBuilder<FlowStep> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasOne(x => x.Transition)
                .WithMany(x => x.Steps)
                .HasForeignKey(x => x.TransitionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.Instance)
                .WithMany(x => x.Steps)
                .HasForeignKey(x => x.InstanceId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
