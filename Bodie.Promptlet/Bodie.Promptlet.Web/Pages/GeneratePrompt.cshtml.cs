using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Bodie.Promptlet.Web.DTO;
using Bodie.Promptlet.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Web;

namespace Bodie.Promptlet.Web.Pages
{

    public class GeneratePromptModel : PageModel
    {
        private readonly PromptletContext _promptletContext;
        private static readonly int MaxVariableLength = 120;
        private readonly ILogger<GeneratePromptModel> _logger;
        public Dictionary<string, string> ReplaceVariableDictionary = new Dictionary<string, string>();


        [BindProperty]
        public PreviewPromptlet PreviewPromptlet { get; set; }
        [BindProperty]
        public string ArtifactCount { get; set; }

        public GeneratePromptModel(ILogger<GeneratePromptModel> logger, PromptletContext promptletContext)
        {
            _promptletContext = promptletContext;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
                ComposedPromptlet promptlet = new ComposedPromptlet();
                if (id != null && id !=-1)
                {
                    promptlet = await _promptletContext.ComposedPromplets
                        .Include("PromptletArtifacts")
                        .FirstOrDefaultAsync(m => m.ComposedPromptletId == id);
                }

                ArtifactCount = $"{promptlet.PromptletArtifacts.Count}";

                ReplaceVariableDictionary.Clear();

                var bodyPartsBuffer = new StringBuilder();

                foreach (var item in promptlet.PromptletArtifacts)
                {
                    ExtractVariableDictionary(
                        item.PromptletArtifactContent, 
                        item.VariableStartDeliminator.ToCharArray()[0], 
                        item.VariableEndDeliminator.ToCharArray()[0]);
                    bodyPartsBuffer.Append(item.PromptletArtifactContent);
                }  

                PreviewPromptlet = new PreviewPromptlet
                {
                    ComposedPromptletId = promptlet.ComposedPromptletId,
                    ComposedPromptletDescription = promptlet.ComposedPromptletDescription,
                    ComposedPromptletName = promptlet.ComposedPromptletName,
                    ComposedPromptletVersion = promptlet.ComposedPromptletVersion,
                    ComposedPromptletHeader = promptlet.ComposedPromptletHeader,
                    ComposedPromptletFooter = promptlet.ComposedPromptletFooter,
                    RawBodyString = HttpUtility.HtmlEncode(bodyPartsBuffer.ToString()),
                    Variables = String.Empty
                };
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var promptlet = PreviewPromptlet;

            IDictionary<string, string> variableSubstitution = new Dictionary<string, string>();

            foreach (var entry in Request.Form)
            {
                if (!entry.Key.StartsWith("_") && !entry.Key.Contains(nameof(PreviewPromptlet)))
                {
                    variableSubstitution.Add(entry.Key, entry.Value.ToString() ?? "");
                }
            }

            var preview = PromptAssembler.AssemblePrompt(promptlet, variableSubstitution);

            return RedirectToPage("./GeneratedPromptlet", new { preview = preview });
        }

        public void ExtractVariableDictionary(string input, char startDelim, char endDelim)
        {

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
                            ReplaceVariableDictionary.TryAdd(valueBuilder.ToString(), "");
                            valueBuilder.Clear();
                            break;
                        }
                    }
                }
            }
        }

    }
}
