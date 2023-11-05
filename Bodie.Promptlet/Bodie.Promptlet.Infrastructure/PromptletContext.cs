using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Bodie.Promptlet.Infrastructure
{
    public class PromptletContext : DbContext
    {
        public PromptletContext(DbContextOptions<PromptletContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }
        public DbSet<PromptletArtifact> PromptletArtifacts { get; set; }
        public DbSet<ComposedPromptlet> ComposedPromplets { get; set; }
        public DbSet<PromptCollection> PromptCollections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromptletArtifact>().ToTable("PromptletArtifact");
            modelBuilder.Entity<ComposedPromptlet>().ToTable("ComposedPromptlet");
            modelBuilder.Entity<PromptCollection>().ToTable("PromptCollection");
        }
    }
}
