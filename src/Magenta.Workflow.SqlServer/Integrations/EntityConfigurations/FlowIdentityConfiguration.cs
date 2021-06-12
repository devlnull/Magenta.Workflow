using Magenta.Workflow.Context.Flows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magenta.Workflow.SqlServer.Integrations.EntityConfigurations
{
    public class FlowIdentityConfiguration :IEntityTypeConfiguration<FlowIdentity>
    {
        public void Configure(EntityTypeBuilder<FlowIdentity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasOne(x => x.State)
                .WithMany(x => x.Identities)
                .HasForeignKey(x => x.StateId)
                .IsRequired();

        }
    }
}
