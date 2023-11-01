using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bodie.Promptlet.Web.Pages
{
    public class AllComposedPromptletModel : PageModel
    {
        private static PromptletContext _promptletContext;

        public ICollection<ComposedPromptlet> ComposedPromptlets;

        public AllComposedPromptletModel(PromptletContext context)
        {
            _promptletContext = context;

        }
        public void OnGet()
        {
            ComposedPromptlets = _promptletContext.ComposedPromplets.ToList();
        }
    }
}
