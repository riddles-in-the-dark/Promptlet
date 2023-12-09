using Microsoft.AspNetCore.Mvc;
using Promptlet.Api.Dto;
using Promptlet.Domain.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromptletCollectionController : ControllerBase
    {
        private readonly IPromptletCollectionDomainService _domainService;

        private readonly ILogger<PromptletCollectionController> _logger;

        public PromptletCollectionController(ILogger<PromptletCollectionController> logger, IPromptletCollectionDomainService domainService)
        {
            _logger = logger;
            _domainService = domainService;
        }

        [HttpGet(Name = "GetPromptletCollections")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<PromptletCollection>> Get()
        {
            var obj = await _domainService.GetAll();
            return obj.ToArray();
        }

        [HttpPut(Name = "UpdatePromptletCollection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<PromptletCollection> Put(UpdatePromptletCollectionRequest request)
        {
            var obj = await _domainService.Update(
                new PromptletCollection
                {
                    PromptletCollectionId = request.PromptletCollectionId,
                    PromptletCollectionName = request.PromptletCollectionName
                });

            return obj;
        }

        [HttpPost(Name = "CreatePromptletCollection")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<PromptletCollection> Post(CreatePromptletCollectionRequest request)
        {
            var obj = await _domainService.Create(
                new PromptletCollection
                {
                    PromptletCollectionId =0,
                    PromptletCollectionName = request.PromptletCollectionName 
                });            
            
            return obj;
        }
    }
    }