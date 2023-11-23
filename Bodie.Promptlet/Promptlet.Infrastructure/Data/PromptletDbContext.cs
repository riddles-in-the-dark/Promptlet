using Microsoft.EntityFrameworkCore;
using Promptlet.Infrastructure.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;


namespace Promptlet.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public class PromptletDbContext : DbContext
    {
        public PromptletDbContext(DbContextOptions<PromptletDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }
        public DbSet<PromptletArtifact> PromptletArtifacts { get; set; }
        public DbSet<ComposedPromptlet> ComposedPromptlets { get; set; }
        public DbSet<PromptletCollection> PromptletCollections { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
