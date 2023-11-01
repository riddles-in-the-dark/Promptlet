using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Bodie.Promptlet.Web.Pages
{
    public class EditPromptletArtifactModel : PageModel
    {
        private readonly PromptletContext _promptletContext;
        private const int MaxVariableLength = 120;

        [BindProperty]
        public PromptletArtifact PromptletArtifact { get; set; }

        public Dictionary<string, string> VariablesInContent = new Dictionary<string, string>();

        [BindProperty]
        public List<PromptletArtifact> PromptletArtifacts { get; set; }

        public EditPromptletArtifactModel(PromptletContext promptletContext)
        {
            _promptletContext = promptletContext;
        }

        public async Task<IActionResult> OnGetAsync(int? id = 1)
        {
            if (id != null)
            {
                PromptletArtifact = await _promptletContext.PromptletArtifacts
                    .Include("ComposedPromptlets")
                    .FirstOrDefaultAsync(m => m.PromptletArtifactId == id);
            }

            if (PromptletArtifact.VariableStartDeliminator != null
                && PromptletArtifact.VariableStartDeliminator != null)
            {
                VariablesInContent = ExtractVariableDictionary(
                    PromptletArtifact.PromptletArtifactContent,
                    PromptletArtifact.VariableStartDeliminator.ToCharArray(0, 1).First(),
                    PromptletArtifact.VariableEndDeliminator.ToCharArray(0, 1).First());
            }
            return Page();
        }

        public static Dictionary<string, string> ExtractVariableDictionary(string input, char startDelim, char endDelim)
        {
            var result = new Dictionary<string, string>();
            var inputArray = input.ToCharArray();
            var valueBuilder = new StringBuilder(MaxVariableLength);
            for (int pos = 0; pos <= inputArray.Length - 1; pos++)
            {
                if (inputArray[pos] == startDelim)
                {
                    valueBuilder.Clear();

                    for (int walk = pos; walk <= pos + MaxVariableLength; walk++)
                    {
                        valueBuilder.Append(inputArray[walk]);

                        if (inputArray[walk] == endDelim)
                        {
                            result.TryAdd(valueBuilder.ToString(), "");
                            valueBuilder.Clear();
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var promptletArtifact = PromptletArtifact;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _promptletContext.Entry(promptletArtifact).Property(x => x.PromptletArtifactName).IsModified = true;
            _promptletContext.Entry(promptletArtifact).Property(x => x.PromptletArtifactDescription).IsModified = true;
            _promptletContext.Entry(promptletArtifact).Property(x => x.PromptletArtifactContent).IsModified = true;
            _promptletContext.Entry(promptletArtifact).Property(x => x.PromptletArtifactOrder).IsModified = true;
            _promptletContext.Entry(promptletArtifact).Property(x => x.PromptletArtifactVersion).IsModified = true;
            _promptletContext.Entry(promptletArtifact).Property(x => x.VariableStartDeliminator).IsModified = true;
            _promptletContext.Entry(promptletArtifact).Property(x => x.VariableEndDeliminator).IsModified = true;
            await _promptletContext.SaveChangesAsync();

            return RedirectToPage("AllComposedPromptlet");
        }

    }
}
