using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.Contracts
{
    public interface IComposedPromptletRepository : IRepository<ComposedPromptlet> 
    {
        Task<ComposedPromptlet?> ReorderPromptletArtifacts(int composedPromptletId, Dictionary<int, int> promptletArtifactsIdNewOrdinal);
    }
}
