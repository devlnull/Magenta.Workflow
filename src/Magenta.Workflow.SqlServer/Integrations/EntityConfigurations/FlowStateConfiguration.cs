using Magenta.Workflow.Context.Flows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magenta.Workflow.SqlServer.Integrations.EntityConfigurations
{
    public class FlowStateConfiguration : IEntityTypeConfiguration<FlowState>
    {
        public void Configure(EntityTypeBuilder<FlowState> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasOne(x => x.Type)
                .WithMany(x => x.States)
                .HasForeignKey(x => x.TypeId)
                .IsRequired();
        }
    }
}
