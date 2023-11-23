using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.Data.DataConfigurations
{
    public class ComposedPromptletConfiguration : IEntityTypeConfiguration<ComposedPromptlet>
    {
        public void Configure(EntityTypeBuilder<ComposedPromptlet> builder)
        {
            // Table Name
            builder.ToTable(nameof(ComposedPromptlet));

            // Entity key
            builder.HasKey(c => c.ComposedPromptletId);

            // Identity column
            builder.Property(c => c.ComposedPromptletId).UseIdentityColumn();
        }
    }
}
