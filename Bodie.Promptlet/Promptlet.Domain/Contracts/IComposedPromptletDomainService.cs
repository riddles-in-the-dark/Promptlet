using Promptlet.Infrastructure.Models;

namespace Promptlet.Domain.Contracts
{
    public interface IComposedPromptletDomainService : IDomainService<ComposedPromptlet> 
    {
        Task<ComposedPromptlet?> ReorderPromptletArtifacts(int composedPromptletId, Dictionary<int, int> promptletArtifactsIdNewOrdinal);
    }
}
