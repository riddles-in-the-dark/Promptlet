using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bodie.Promptlet.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private static PromptletContext _promptletContext;

        public ICollection<ComposedPromptlet> ComposedPromptlets;

        public void OnGet()
        {
            ComposedPromptlets = _promptletContext.ComposedPromplets.ToList();
        }

        public IndexModel(ILogger<IndexModel> logger, PromptletContext context)
        {
            _logger = logger; _promptletContext = context;
        }

    }
}