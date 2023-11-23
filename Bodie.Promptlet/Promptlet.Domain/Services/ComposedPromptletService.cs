using Promptlet.Domain.Contracts;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Domain.Services
{
    public class ComposedPromptletService : IComposedPromptletDomainService
    {
        public readonly IComposedPromptletRepository _repository;

        public ComposedPromptletService(IComposedPromptletRepository repository)
        {
            _repository = repository;
        }
        public async Task<ComposedPromptlet?> Create(ComposedPromptlet _object)
        {
            return await _repository.Create(_object);
        }

        public async Task Delete(ComposedPromptlet _object)
        {
            await _repository.Delete(_object);
        }

        public async Task<IEnumerable<ComposedPromptlet>?> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ComposedPromptlet?> GetById(int Id)
        {
           return await _repository.GetById(Id);
        }

        public async Task<ComposedPromptlet?> ReorderPromptletArtifacts(int composedPromptletId, Dictionary<int, int> promptletArtifactsIdNewOrdinal)
        {
            return await _repository.ReorderPromptletArtifacts(composedPromptletId, promptletArtifactsIdNewOrdinal);
        }

        public async Task<ComposedPromptlet?> Update(ComposedPromptlet _object)
        {
            return await _repository.Update(_object);
        }
    }
}