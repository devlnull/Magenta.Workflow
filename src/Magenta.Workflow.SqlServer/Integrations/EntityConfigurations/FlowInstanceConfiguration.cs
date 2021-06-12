using Magenta.Workflow.Context.Flows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magenta.Workflow.SqlServer.Integrations.EntityConfigurations
{
    public class FlowInstanceConfiguration : IEntityTypeConfiguration<FlowInstance>
    {
        public void Configure(EntityTypeBuilder<FlowInstance> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasOne(x => x.Type)
                .WithMany(x => x.Instances)
                .HasForeignKey(x => x.TypeId)
                .IsRequired();

        }
    }
}
