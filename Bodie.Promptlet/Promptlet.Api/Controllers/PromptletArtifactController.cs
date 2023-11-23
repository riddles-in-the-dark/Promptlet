using Microsoft.AspNetCore.Mvc;
using Promptlet.Domain.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromptletArtifactController : ControllerBase
    {
        private readonly IPromptletArtifactDomainService _domainService;

        private readonly ILogger<PromptletArtifactController> _logger;

        public PromptletArtifactController(ILogger<PromptletArtifactController> logger, IPromptletArtifactDomainService domainService)
        {
            _logger = logger;
            _domainService = domainService;
        }

        [HttpGet(Name = "GetPromptletArtifacts")]
        public async Task<IEnumerable<PromptletArtifact>> Get()
        {
            var obj = await _domainService.GetAll();
            return obj.ToArray();
        }

        [HttpPut(Name = "UpdatePromptletArtifact")]
        public async Task<PromptletArtifact> Put(PromptletArtifact promptletArtifact)
        {
            var obj = await _domainService.Update(promptletArtifact);
            return obj;
        }

        [HttpPost(Name = "CreatePromptletArtifact")]
        public async Task<PromptletArtifact> Post(PromptletArtifact promptletArtifact)
        {
            var obj = await _domainService.Create(promptletArtifact);
            return obj;
        }
    }
    }