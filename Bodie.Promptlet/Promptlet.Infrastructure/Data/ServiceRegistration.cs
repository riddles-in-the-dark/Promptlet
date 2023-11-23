using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Models;
using Promptlet.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Promptlet.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistration
    {
        public static void AddPrompletContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PromptletDbContext>(options => options.UseSqlServer(connectionString));
        }
        public static void AddPrompletDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PromptletDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IPromptletCollectionRepository, PromptletCollectionRepository>();
            services.AddScoped<IComposedPromptletRepository, ComposedPromptletRepository>();
            services.AddScoped<IPromptletArtifactRepository, PromptletArtifactRepository>();

        }
    }
}
