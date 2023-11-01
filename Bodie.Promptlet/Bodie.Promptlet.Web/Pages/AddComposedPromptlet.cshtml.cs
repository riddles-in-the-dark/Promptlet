using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bodie.Promptlet.Web.Pages
{
    public class AddComposedPromptletModel : PageModel
    {
        private readonly PromptletContext _promptletContext;

        [BindProperty]
        public ComposedPromptlet ComposedPromptlet { get; set; }
        [BindProperty]
        public List<PromptletArtifact> PromptletArtifacts { get; set; }

        public AddComposedPromptletModel(PromptletContext promptletContext)
        {
            _promptletContext = promptletContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ComposedPromptlet = new ComposedPromptlet();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var newComposedPromptletEntity = await _promptletContext.AddAsync(ComposedPromptlet);


            if (!ModelState.IsValid)
            {
                return Page();
            }

            // newComposedPromptletEntity.ComposedPromptletVersion += 1;
            //_promptletContext.Entry(existingComposedPromptletEntity).Property(x => x.ComposedPromptletName).IsModified = true;
            //_promptletContext.Entry(existingComposedPromptletEntity).Property(x => x.ComposedPromptletDescription).IsModified = true;
            //_promptletContext.Entry(existingComposedPromptletEntity).Property(x => x.ComposedPromptletHeader).IsModified = true;
            //_promptletContext.Entry(existingComposedPromptletEntity).Property(x => x.ComposedPromptletFooter).IsModified = true;
            //_promptletContext.Entry(existingComposedPromptletEntity).Property(x => x.ComposedPromptletVersion).IsModified = true;
            //_promptletContext.Entry(existingComposedPromptletEntity).Property(x => x.PromptletArtifacts).IsModified = true;
            await _promptletContext.SaveChangesAsync();

            return RedirectToPage("AllComposedPromptlet");
        }

    }

}
