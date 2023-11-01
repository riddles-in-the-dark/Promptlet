using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bodie.Promptlet.Web.Pages
{
    public class GeneratedPromptletModel : PageModel
    {
        [BindProperty]
        public string Preview { get; set; }

        public async Task<IActionResult> OnGetAsync(string preview)
        {

            Preview = preview;

            return Page();
        }
    }
}
