using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.Data.DataConfigurations
{
    public class PromptletCollectionConfiguration : IEntityTypeConfiguration<PromptletCollection>
    {
        public void Configure(EntityTypeBuilder<PromptletCollection> builder)
        {
            // Table Name
            builder.ToTable(nameof(PromptletCollection));

            // Entity key
            builder.HasKey(c => c.PromptletCollectionId);

            // Identity column
            builder.Property(c => c.PromptletCollectionId).UseIdentityColumn();

        }
    }
}
