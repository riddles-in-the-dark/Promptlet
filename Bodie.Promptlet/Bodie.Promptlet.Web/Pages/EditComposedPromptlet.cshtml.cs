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

        [BindProperty]
        public List<PromptCollection> PromptCollections { get; set; }

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
                    .Include("PromptCollections")
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ComposedPromptletId == id);

                PromptCollections = ComposedPromptlet.PromptCollections.ToList();
                PromptletArtifacts = ComposedPromptlet.PromptletArtifacts.ToList();

            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           // var composedPromptlet = ComposedPromptlet;


            var existingComposedPromptletEntity = await _promptletContext.Set<ComposedPromptlet>()
                .Where(x => x.ComposedPromptletId == ComposedPromptlet.ComposedPromptletId)
                .Include(x => x.PromptletArtifacts)
                .SingleOrDefaultAsync();

            if (existingComposedPromptletEntity != null)
            {

                var artifactCollection = new Dictionary<string, string>();
                var promptCollection = new Dictionary<string, string>();

                foreach (var entry in Request.Form)
                {
                    if (!entry.Key.StartsWith("_"))
                    {
                        artifactCollection.Add(entry.Key, entry.Value.ToString() ?? "");
                    }
                    if (entry.Key.Contains(nameof(ComposedPromptlet)))
                    {
                        promptCollection.Add(entry.Key, entry.Value.ToString() ?? "");
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

                if(Int32.TryParse(promptCollection["ComposedPromptlet.ComposedPromptletVersion"], out int ver))
                {
                    existingComposedPromptletEntity.ComposedPromptletVersion = ver;
                }


                existingComposedPromptletEntity.ComposedPromptletName = promptCollection["ComposedPromptlet.ComposedPromptletName"];
                existingComposedPromptletEntity.ComposedPromptletDescription = promptCollection["ComposedPromptlet.ComposedPromptletDescription"];
                existingComposedPromptletEntity.ComposedPromptletHeader = promptCollection["ComposedPromptlet.ComposedPromptletHeader"];
                existingComposedPromptletEntity.ComposedPromptletFooter = promptCollection["ComposedPromptlet.ComposedPromptletFooter"];

     
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }


            await _promptletContext.SaveChangesAsync();

            return RedirectToPage("AllComposedPromptlet");
        }

    }

}
