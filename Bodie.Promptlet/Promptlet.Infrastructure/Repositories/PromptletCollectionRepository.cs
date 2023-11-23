using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Data;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.Repositories
{
    public class PromptletCollectionRepository : IPromptletCollectionRepository
    {
        private readonly PromptletDbContext _context;
        private readonly ILogger _logger;

        public PromptletCollectionRepository(ILogger<PromptletCollectionRepository> logger, PromptletDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PromptletCollection?> Create(PromptletCollection _object)
        {
            _logger?.LogDebug($"'{nameof(PromptletCollectionRepository)}.'{nameof(Create)}' has been invoked");

            try
            {
                using var ctx = _context;
                var obj = await ctx.AddAsync<PromptletCollection>(_object);
                await ctx.SaveChangesAsync();
                return obj.Entity;

            }
            catch (Exception) 
            { 
                throw; 
            }
        }

        public async Task Delete(PromptletCollection _object)
        {
            _logger?.LogDebug($"'{nameof(PromptletCollectionRepository)}.'{nameof(Delete)}' has been invoked");
           
            try
            {
                using var ctx = _context;
                var obj = ctx.Remove<PromptletCollection>(_object);
                if(obj != null)
                {
                    await ctx.SaveChangesAsync();
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PromptletCollection>?> GetAll()
        {
            _logger?.LogDebug($"'{nameof(PromptletCollectionRepository)}.'{nameof(GetAll)}' has been invoked");

            try
            {
                using var ctx = _context;
                return await ctx.PromptletCollections
                    .Include(x => x.ComposedPromptlets)
                    .ThenInclude(y => y.PromptletArtifacts)
                .ToListAsync()
                 ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PromptletCollection?> GetById(int Id)
        {
            _logger?.LogDebug($"'{nameof(PromptletCollectionRepository)}.'{nameof(GetById)}' has been invoked");

            try
            {
                using var ctx = _context;
                return await ctx.PromptletCollections.FirstOrDefaultAsync(x => x.PromptletCollectionId == Id) ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PromptletCollection?> Update(PromptletCollection _object)
        {
            _logger?.LogDebug($"'{nameof(PromptletCollectionRepository)}.'{nameof(Update)}' has been invoked");

            try
            {
                using var ctx = _context;
                var obj = ctx.Update<PromptletCollection>(_object);
                if(obj != null) await  ctx.SaveChangesAsync();
                return obj?.Entity ?? null;                
            }
            catch(Exception) 
            {
                throw;
            }
        }
    }
}
