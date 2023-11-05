using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bodie.Promptlet.Web.Pages
{
    public class AllPromptCollectionsModel : PageModel
    {
        private static PromptletContext _promptletContext;

        public ICollection<PromptCollection> PromptCollections;

        public AllPromptCollectionsModel(PromptletContext context)
        {
            _promptletContext = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            PromptCollections = _promptletContext.PromptCollections
                .Include("ComposedPromptlets")
                .ToList();
            return Page();
        }
    }
}
