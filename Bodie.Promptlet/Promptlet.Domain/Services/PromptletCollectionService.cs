using Promptlet.Domain.Contracts;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Domain.Services
{
    public class PromptletCollectionService : IPromptletCollectionDomainService
    {
        public readonly IPromptletCollectionRepository _repository;

        public PromptletCollectionService(IPromptletCollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<PromptletCollection?> GetById(int id) 
        {
           return await _repository.GetById(id);
        }

        public async Task<IEnumerable<PromptletCollection>?> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<PromptletCollection?> Create(PromptletCollection promptletCollection)
        {
            return await _repository.Create(promptletCollection);
        }

        public async Task<PromptletCollection?> Update(PromptletCollection promptletCollection)
        {
            return await _repository.Update(promptletCollection);
        }

        public async Task Delete(PromptletCollection promptletCollection)
        {
            await _repository.Delete(promptletCollection);
        }
    }
}
