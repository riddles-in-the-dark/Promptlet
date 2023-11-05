using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bodie.Promptlet.Web.Pages
{
    public class AddComposedPromptletModel : PageModel
    {
        private readonly PromptletContext _promptletContext;

        [BindProperty]
        public PromptCollection PromptCollection { get; set; }

        [BindProperty]
        public ComposedPromptlet ComposedPromptlet { get; set; }
        [BindProperty]
        public List<PromptletArtifact> PromptletArtifacts { get; set; }

        public AddComposedPromptletModel(PromptletContext promptletContext)
        {
            _promptletContext = promptletContext;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                PromptCollection = await _promptletContext.PromptCollections
                    .Include("ComposedPromptlets")
                    .FirstOrDefaultAsync(predicate: m => m.PromptCollectionId == id);
            }

              ComposedPromptlet = new ComposedPromptlet();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            //  {
            //      return Page();
            // }

            ComposedPromptlet.ComposedPromptletVersion += 1;

            if (PromptCollection != null)
            {
                var promptCollection = await _promptletContext.FindAsync<PromptCollection>(PromptCollection.PromptCollectionId);
                promptCollection.ComposedPromptlets.Add(ComposedPromptlet);
                _promptletContext.Update(promptCollection);
            }
            else
            {
                await _promptletContext.AddAsync(ComposedPromptlet);
            }

            await _promptletContext.SaveChangesAsync();
        
            return RedirectToPage("AllComposedPromptlet");
        }

    }

}
