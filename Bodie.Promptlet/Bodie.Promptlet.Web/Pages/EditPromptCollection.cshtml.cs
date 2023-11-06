using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bodie.Promptlet.Web.Pages
{
    public class EditPromptCollectionModel : PageModel
    {
 
        private readonly PromptletContext _promptletContext;

        [BindProperty]
        public PromptCollection PromptCollection { get; set; }

        public EditPromptCollectionModel(PromptletContext promptletContext)
        {
            _promptletContext = promptletContext;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                PromptCollection = await _promptletContext.PromptCollections
                    .FirstOrDefaultAsync(m => m.PromptCollectionId == id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _promptletContext.Entry(PromptCollection).Property(x => x.PromptCollectionName).IsModified = true;
            await _promptletContext.SaveChangesAsync();

            return RedirectToPage("Index");
        }


    }
}
