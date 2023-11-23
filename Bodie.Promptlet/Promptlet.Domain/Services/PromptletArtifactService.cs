using Promptlet.Domain.Contracts;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Domain.Services
{
    public class PromptletArtifactService : IPromptletArtifactDomainService
    {
        public readonly IPromptletArtifactRepository _repository;

        public PromptletArtifactService(IPromptletArtifactRepository repository)
        {
            _repository = repository;
        }

        public async Task<PromptletArtifact?> Create(PromptletArtifact _object)
        {
            return await _repository.Create(_object);
        }

        public async Task Delete(PromptletArtifact _object)
        {
            await _repository.Delete(_object);
        }

        public async Task<IEnumerable<PromptletArtifact>?> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<PromptletArtifact?> GetById(int Id)
        {
            return await _repository.GetById(Id);
        }

        public async Task<PromptletArtifact?> Update(PromptletArtifact _object)
        {
            return await _repository.Update(_object);
        }
    }
}