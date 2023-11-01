using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bodie.Promptlet.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddPrompletContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PromptletContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
