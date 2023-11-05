using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bodie.Promptlet.Web.Pages
{
    public class AddPromptCollectionModel : PageModel
    {
 
        private readonly PromptletContext _promptletContext;

        [BindProperty]
        public PromptCollection PromptCollection { get; set; }

        public AddPromptCollectionModel(PromptletContext promptletContext)
        {
            _promptletContext = promptletContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            PromptCollection = new PromptCollection();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

           await _promptletContext.AddAsync(PromptCollection);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _promptletContext.SaveChangesAsync();

            return RedirectToPage("Index");
        }


    }
}
