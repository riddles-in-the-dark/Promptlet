using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bodie.Promptlet.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private static PromptletContext _promptletContext;

       public ICollection<PromptCollection> PromptCollections;



        public void OnGet()
        {
           PromptCollections = _promptletContext.PromptCollections.Include("ComposedPromptlets").ToList();
     
        }

        public IndexModel(ILogger<IndexModel> logger, PromptletContext context)
        {
            _logger = logger; _promptletContext = context;
        }

    }
}