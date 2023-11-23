using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Data;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.Repositories
{
    public class ComposedPromptletRepository : IComposedPromptletRepository
    {
        private readonly PromptletDbContext _context;
        private readonly ILogger _logger;

        public ComposedPromptletRepository(ILogger<ComposedPromptletRepository> logger, PromptletDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ComposedPromptlet?> Create(ComposedPromptlet _object)
        {
            _logger.LogDebug($"'{nameof(ComposedPromptletRepository)}.'{nameof(Create)}' has been invoked.");

            try
            {
                using var ctx = _context;
                var obj = await ctx.AddAsync<ComposedPromptlet>(_object);
                await ctx.SaveChangesAsync();
                return obj.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(ComposedPromptlet _object)
        {
            _logger.LogDebug($"'{nameof(ComposedPromptletRepository)}.'{nameof(Delete)}' has been invoked.");

            try
            {
                using var ctx = _context;
                var obj = ctx.Remove<ComposedPromptlet>(_object);
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

        public async Task<IEnumerable<ComposedPromptlet>?> GetAll()
        {
            _logger.LogDebug($"'{nameof(ComposedPromptletRepository)}.'{nameof(GetAll)}' has been invoked.");

            try
            {
                using var ctx = _context;   
                return await ctx.ComposedPromptlets
                .ToListAsync()
                 ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ComposedPromptlet?> GetById(int Id)
        {
            _logger.LogDebug($"'{nameof(ComposedPromptletRepository)}.'{nameof(GetById)}' has been invoked.");

            try
            {
                using var ctx = _context;
                return await ctx.ComposedPromptlets.FirstOrDefaultAsync(x => x.ComposedPromptletId == Id) ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ComposedPromptlet?> Update(ComposedPromptlet _object)
        {
            _logger.LogDebug($"'{nameof(ComposedPromptletRepository)}.'{nameof(Update)}' has been invoked.");

            try
            {
                using var ctx = _context;
                var obj = ctx.Update<ComposedPromptlet>(_object);
                if (obj != null) await ctx.SaveChangesAsync();
                return obj?.Entity ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ComposedPromptlet?> ReorderPromptletArtifacts(int composedPromptletId, Dictionary<int,int> promptletArtifactsIdNewOrdinal)
        {
            _logger.LogDebug($"'{nameof(ComposedPromptletRepository)}.'{nameof(ReorderPromptletArtifacts)}' has been invoked.");

            try
            {
                using var ctx = _context;
                var existingComposedPromptletEntity = await ctx.Set<ComposedPromptlet>()
                    .Where(x => x.ComposedPromptletId == composedPromptletId)
                    .Include(x => x.PromptletArtifacts)
                    .SingleOrDefaultAsync();

                for (int i = 0; i <= promptletArtifactsIdNewOrdinal.Count - 1; i++)
                {
                    var artifact = existingComposedPromptletEntity.PromptletArtifacts.Where(x => x.PromptletArtifactId == promptletArtifactsIdNewOrdinal[i]).SingleOrDefault();

                    artifact.PromptletArtifactOrder = i + 1;

                    if (artifact != null)
                    {
                        ctx.Entry<PromptletArtifact>(artifact).CurrentValues.SetValues(artifact.PromptletArtifactOrder);
                    }
                }
                await ctx.SaveChangesAsync();
                return existingComposedPromptletEntity ?? null;
            }
            catch (Exception) 
            {
                throw;
            }
        }
    }
}
