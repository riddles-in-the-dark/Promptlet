using Promptlet.Api.Dto;

namespace Promptlet.Api.Client
{
    public interface IPromptletApiClient
    {
        Task<PromptletCollectionsResponse> GetAllPromptletCollections();
    }
    
   
}