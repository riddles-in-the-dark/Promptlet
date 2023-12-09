using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Promptlet.Api.Dto;

namespace Promptlet.Api.Client
{

    public sealed class PromptletApiClient : IPromptletApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory = null;
        private readonly ApiClientConfiguration _apiClientConfiguration;
        private readonly ILogger<PromptletApiClient> _logger;
        private readonly string _baseUrl;
        public PromptletApiClient(IHttpClientFactory httpClientFactory, ILogger<PromptletApiClient> logger, ApiClientConfiguration apiClientConfiguration)
        {
            _apiClientConfiguration = apiClientConfiguration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _baseUrl = apiClientConfiguration.ApiBaseUrl ?? string.Empty;
        }


        public async Task<PromptletCollectionsResponse> GetAllPromptletCollections()
        {
            var _client = _httpClientFactory.CreateClient(nameof(PromptletApiClient));
          
            var response = new PromptletCollectionsResponse();
            try
            {
              var str = _client.GetStringAsync("PromptletCollection");
            }
            catch (Exception ex) 
            {
                _logger.LogError($"An Error occured in {nameof(GetAllPromptletCollections)}",ex);
            }
            return response;
        }        
      
    }
    
   
}