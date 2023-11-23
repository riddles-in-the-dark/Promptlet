using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Promptlet.Domain.Contracts;
using Promptlet.Domain.Services;
using Promptlet.Infrastructure.Data;
using Promptlet.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promptlet.Domain.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPromptletDomainModule(this IServiceCollection services, string connectionString)
        {
            services.AddPrompletDbContext(connectionString);
            services.AddScoped<IPromptletCollectionDomainService, PromptletCollectionService>();
            services.AddScoped<IComposedPromptletDomainService, ComposedPromptletService>();
            services.AddScoped<IPromptletArtifactDomainService, PromptletArtifactService>();
        }

    }
}
