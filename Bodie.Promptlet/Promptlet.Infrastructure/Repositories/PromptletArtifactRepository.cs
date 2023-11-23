using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Data;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.Repositories
{
    public class PromptletArtifactRepository : IPromptletArtifactRepository
    {
        private readonly PromptletDbContext _context;
        private readonly ILogger _logger;

        public PromptletArtifactRepository(ILogger<PromptletArtifactRepository> logger, PromptletDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<PromptletArtifact?> Create(PromptletArtifact _object)
        {
            _logger.LogDebug($"'{nameof(PromptletArtifactRepository)}.{nameof(Create)}' has been invoked.");
            try
            {
                using var ctx = _context;
                var obj = await ctx.AddAsync<PromptletArtifact>(_object);
                await ctx.SaveChangesAsync();
                return obj.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(PromptletArtifact _object)
        {
            _logger.LogDebug($"'{nameof(PromptletArtifactRepository)}.'{nameof(Delete)}' has been invoked.");
            try
            {
                using var ctx = _context;
                var obj = ctx.Remove<PromptletArtifact>(_object);
                if (obj != null)
                {
                    await ctx.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PromptletArtifact>?> GetAll()
        {
            _logger.LogDebug($"'{nameof(PromptletArtifactRepository)}.'{nameof(GetAll)}' has been invoked.");
            try
            {
                using var ctx = _context;
                return await ctx.PromptletArtifacts
                .ToListAsync()
                 ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PromptletArtifact?> GetById(int Id)
        {
            _logger.LogDebug($"'{nameof(PromptletArtifactRepository)}.'{nameof(GetById)}' has been invoked.");
            try
            {
                using var ctx = _context;
                return await ctx.PromptletArtifacts.FirstOrDefaultAsync(x => x.PromptletArtifactId == Id) ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PromptletArtifact?> Update(PromptletArtifact _object)
        {
            _logger.LogDebug($"'{nameof(PromptletArtifactRepository)}.'{nameof(Update)}' has been invoked.");
            try
            {
                using var ctx = _context;
                var obj = ctx.Update<PromptletArtifact>(_object);
                if (obj != null) await ctx.SaveChangesAsync();
                return obj?.Entity ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
