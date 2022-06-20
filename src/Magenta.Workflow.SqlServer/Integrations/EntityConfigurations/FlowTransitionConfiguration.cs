using Magenta.Workflow.Context.Flows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magenta.Workflow.SqlServer.Integrations.EntityConfigurations
{
    public class FlowTransitionConfiguration : IEntityTypeConfiguration<FlowTransition>
    {
        public void Configure(EntityTypeBuilder<FlowTransition> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasOne(x => x.Source)
                .WithMany(x => x.Sources)
                .HasForeignKey(x => x.SourceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Destination)
                .WithMany(x => x.Destinations)
                .HasForeignKey(x => x.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FlowType)
                .WithMany(x => x.Transitions)
                .HasForeignKey(x=>x.TypeId)
                .IsRequired();
        }
    }
}
