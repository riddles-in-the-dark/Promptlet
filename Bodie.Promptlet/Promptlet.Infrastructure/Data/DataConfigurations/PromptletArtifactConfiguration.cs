using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.Data.DataConfigurations
{
    public class PromptletArtifactConfiguration : IEntityTypeConfiguration<PromptletArtifact>
    {
        public void Configure(EntityTypeBuilder<PromptletArtifact> builder)
        {
            // Table Name
            builder.ToTable(nameof(PromptletArtifact));

            // Entity key
            builder.HasKey(c => c.PromptletArtifactId);

            // Identity column
            builder.Property(c => c.PromptletArtifactId).UseIdentityColumn();

        }
    }
}
