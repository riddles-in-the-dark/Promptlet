using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Promptlet.Api.Dto;
using Promptlet.Domain.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComposedPromptletController : ControllerBase
    {
        private readonly IComposedPromptletDomainService _domainService;

        private readonly ILogger<ComposedPromptletController> _logger;

        public ComposedPromptletController(ILogger<ComposedPromptletController> logger, IComposedPromptletDomainService domainService)
        {
            _logger = logger;
            _domainService = domainService;
        }

        [HttpGet(Name = "GetComposedPromptlets")]
        public async Task<IEnumerable<ComposedPromptlet>> Get()
        {
            var obj = await _domainService.GetAll();
            return obj.ToArray();
        }

        [HttpPut(Name = "UpdateComposedPromptlet")]
        public async Task<ComposedPromptlet> Put(UpdateComposedPromptletRequest request)
        {
            var obj = await _domainService.Update(
                new ComposedPromptlet 
                { 
                    ComposedPromptletId=request.ComposedPromptletId,
                    ComposedPromptletName=request.ComposedPromptletName,
                    ComposedPromptletDescription=request.ComposedPromptletDescription,
                    ComposedPromptletHeader=request.ComposedPromptletHeader,
                    ComposedPromptletFooter=request.ComposedPromptletFooter
                });
            return obj;
        }

        [HttpPost(Name = "CreateComposedPromptlet")]
        public async Task<ComposedPromptlet> Post(CreateComposedPromptletRequest request)
        {
            var obj = await _domainService.Create(
                new ComposedPromptlet
                {
                    ComposedPromptletName = request.ComposedPromptletName,
                    ComposedPromptletDescription = request.ComposedPromptletDescription,
                    ComposedPromptletHeader = request.ComposedPromptletHeader,
                    ComposedPromptletFooter = request.ComposedPromptletFooter
                });
            return obj;
        }

    }
}