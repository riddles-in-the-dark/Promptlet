using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bodie.Promptlet.Web.Pages
{
    public class AddPromptletArtifactModel : PageModel
    {
        private readonly PromptletContext _promptletContext;
        private readonly ILogger _logger;

        [BindProperty]
        public ComposedPromptlet ComposedPromptlet { get; set; }
        [BindProperty]
        public PromptletArtifact PromptletArtifact { get; set; }

        public AddPromptletArtifactModel(ILogger<AddComposedPromptletModel> logger, PromptletContext promptletContext)
        {
            _logger = logger;
            _promptletContext = promptletContext;
        }

        public async Task<IActionResult> OnGetAsync(int? id = 1)
        {
            if (id != null)
            {
                ComposedPromptlet = await _promptletContext.ComposedPromplets
                    .Include("PromptletArtifacts")
                    .FirstOrDefaultAsync(m => m.ComposedPromptletId == id);
            }
            var maxOrder = 1;


            if (ComposedPromptlet.PromptletArtifacts.Any())
            {
                maxOrder = ComposedPromptlet.PromptletArtifacts.Max(x => x.PromptletArtifactOrder) + 1;
            }

            PromptletArtifact = new PromptletArtifact()
            {
                PromptletArtifactOrder = maxOrder,
                PromptletArtifactVersion = 1,
                VariableStartDeliminator = "[",
                VariableEndDeliminator = "]"
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var composedPromptlet = await _promptletContext.FindAsync<ComposedPromptlet>(ComposedPromptlet.ComposedPromptletId);
                composedPromptlet.PromptletArtifacts.Add(PromptletArtifact);

                _promptletContext.Update(composedPromptlet);
                await _promptletContext.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }


            return RedirectToPage("AllComposedPromptlet");
        }

    }
}
