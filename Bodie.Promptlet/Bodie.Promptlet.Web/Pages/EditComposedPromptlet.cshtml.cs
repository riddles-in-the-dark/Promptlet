using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bodie.Promptlet.Web.Pages
{
    public class EditComposedPromptletModel : PageModel
    {
        private readonly PromptletContext _promptletContext;

        [BindProperty]
        public ComposedPromptlet ComposedPromptlet { get; set; }
        [BindProperty]
        public List<PromptletArtifact> PromptletArtifacts { get; set; }

        public EditComposedPromptletModel(PromptletContext promptletContext)
        {
            _promptletContext = promptletContext;
        }

        public async Task<IActionResult> OnGetAsync(int? id = 1)
        {
            if (id != null)
            {
                ComposedPromptlet = await _promptletContext.ComposedPromplets
                    .Include("PromptletArtifacts")
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ComposedPromptletId == id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var composedPromptlet = ComposedPromptlet;


            var existingComposedPromptletEntity = await _promptletContext.Set<ComposedPromptlet>()
                .Where(x => x.ComposedPromptletId == ComposedPromptlet.ComposedPromptletId)
                .Include(x => x.PromptletArtifacts)
                .SingleOrDefaultAsync();

            if (existingComposedPromptletEntity != null)
            {

                //order, id
                var origOrder = new Dictionary<int, int>();

                foreach (var artifact in existingComposedPromptletEntity.PromptletArtifacts)
                {
                    origOrder.Add(artifact.PromptletArtifactOrder, artifact.PromptletArtifactId);
                }

                var artifactCollection = new Dictionary<string, string>();

                foreach (var entry in Request.Form)
                {
                    if (!entry.Key.StartsWith("_"))//&& !entry.Key.Contains(nameof(ComposedPromptlet)))
                    {
                        artifactCollection.Add(entry.Key, entry.Value.ToString() ?? "");
                    }
                }

                var orderedArtifactIdArray = artifactCollection["PromptletArtifactId"].Split(",").ToArray();
                //order, id
                var updatedOrder = new Dictionary<int, int>();
                for (int i = 0; i <= orderedArtifactIdArray.Length - 1; i++)
                {
                    if (int.TryParse(orderedArtifactIdArray[i], out int id))
                        updatedOrder.Add(i + 1, id);

                    var artifact = existingComposedPromptletEntity.PromptletArtifacts.Where(x => x.PromptletArtifactId == id).SingleOrDefault();

                    artifact.PromptletArtifactOrder = i + 1;

                    if (artifact != null)
                    {
                        _promptletContext.Entry<PromptletArtifact>(artifact).CurrentValues.SetValues(artifact.PromptletArtifactOrder);
                    }


                }



            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            existingComposedPromptletEntity.ComposedPromptletVersion += 1;
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
