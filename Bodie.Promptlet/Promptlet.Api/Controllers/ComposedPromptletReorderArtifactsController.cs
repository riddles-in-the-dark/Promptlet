using Microsoft.AspNetCore.Mvc;
using Promptlet.Api.Dto;
using Promptlet.Domain.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComposedPromptletReorderArtifactsController : ControllerBase
    {
        private readonly IComposedPromptletDomainService _domainService;

        private readonly ILogger<ComposedPromptletReorderArtifactsController> _logger;

        public ComposedPromptletReorderArtifactsController(ILogger<ComposedPromptletReorderArtifactsController> logger, IComposedPromptletDomainService domainService)
        {
            _logger = logger;
            _domainService = domainService;
        }

        [HttpPost(Name = "ComposedPromptletReorderArtifacts")]
        public async Task<ComposedPromptlet> Post(ComposedPromptletReorderArtifactsRequest request)
        {
            var obj = await _domainService.ReorderPromptletArtifacts(request.ComposedPromptletId, request.PromptletArtifactIdNewOrder);
            return obj;
        }


    }
}