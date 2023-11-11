using Bodie.Promptlet.Infrastructure;
using Bodie.Promptlet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace Bodie.Promptlet.Web.Pages.ImportExport
{
    public class ImportCsvPromptletsModel : PageModel
    {
        private readonly PromptletContext _promptletContext;

        [BindProperty]
        public List<CsvModel> CsvModel { get; set; } = new List<CsvModel>();

        [BindProperty]
        public List<PromptCollection> PromptCollections { get; set; }

        [BindProperty]
        public int SelectedPromptCollectionIndex { get; set; }


        [BindProperty]
        public string ExportString { get; set; } = "";

        [BindProperty]
        public string ImportString { get; set; } = "";

        public ImportCsvPromptletsModel(PromptletContext promptletContext)
        {
            _promptletContext = promptletContext;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            PromptCollections = _promptletContext.PromptCollections
                .Include(coll => coll.ComposedPromptlets)
                .ThenInclude(pro => pro.PromptletArtifacts)
                .ToList();


            List<CsvModel> modelss = new List<CsvModel>();
            SelectedPromptCollectionIndex = 2;
 
                ExportString = GeneratePromptCollectionCSV();

            return Page();
        }

        // Define a helper function to generate the CSV content for a PromptCollection
        string GeneratePromptCollectionCSV()
        {
                                 
            StringBuilder csvContent = new();

            // Add the headers for the ComposedPromptlet CSV columns
            csvContent.AppendLine(@"PromptCollectionName, ComposedPromptletName, ComposedPromptletVersion, ComposedPromptletDescription, ComposedPromptletHeader, ComposedPromptletFooter, PromptletArtifactOrder, PromptletArtifactName, PromptletArtifactVersion, PromptletArtifactDescription, PromptletArtifactContent, VariableStartDeliminator, VariableEndDeliminator");
            csvContent.Append(Environment.NewLine);
            foreach (var promptCollection in PromptCollections)
            {
                foreach (var promptlet in promptCollection.ComposedPromptlets)
                {
                    foreach (var promptletArtifact in promptlet.PromptletArtifacts)
                    {
                        CsvModel.Add(
                            new CsvModel
                            {
                                PromptCollectionName = promptCollection.PromptCollectionName,
                                ComposedPromptletName = promptlet.ComposedPromptletName,
                                ComposedPromptletVersion = promptlet.ComposedPromptletVersion.ToString(),
                                ComposedPromptletDescription = promptlet.ComposedPromptletDescription ?? "",
                                ComposedPromptletHeader = promptlet.ComposedPromptletFooter ?? "",
                                PromptletArtifactOrder = promptletArtifact.PromptletArtifactOrder.ToString(),
                                PromptletArtifactName = promptletArtifact.PromptletArtifactName,
                                PromptletArtifactVersion = promptletArtifact.PromptletArtifactVersion.ToString(),
                                PromptletArtifactDescription = promptletArtifact.PromptletArtifactDescription ?? "",
                                PromptletArtifactContent = promptletArtifact.PromptletArtifactContent ?? "",
                                VariableStartDeliminator = promptletArtifact.VariableStartDeliminator ?? "",
                                VariableEndDeliminator = promptletArtifact.VariableEndDeliminator ?? ""
                                }) ;
                    }                            
                    //  csvContent.AppendLine($"{promptlet.PromptCollectionName}, {promptlet.ComposedPromptletName}, {promptlet.ComposedPromptletVersion}, {promptlet.ComposedPromptletDescription}, {promptlet.ComposedPromptletHeader}, {promptlet.ComposedPromptletFooter}, {promptlet.PromptletArtifactOrder}, {promptlet.PromptletArtifactName}, {promptlet.PromptletArtifactVersion}, {promptlet.PromptletArtifactDescription}, {promptlet.PromptletArtifactContent}, {promptlet.VariableStartDeliminator}, {promptlet.VariableEndDeliminator}");
                }
            }
            foreach(var promptlet in CsvModel.ToList().OrderBy(x=>x.PromptCollectionName).OrderBy(x => x.ComposedPromptletName).OrderBy(x => x.PromptletArtifactOrder))
            {
                csvContent.Append(Environment.NewLine);
                csvContent.AppendLine($"{promptlet.PromptCollectionName}, {promptlet.ComposedPromptletName}, {promptlet.ComposedPromptletVersion}, {promptlet.ComposedPromptletDescription}, {promptlet.ComposedPromptletHeader}, {promptlet.ComposedPromptletFooter}, {promptlet.PromptletArtifactOrder}, {promptlet.PromptletArtifactName}, {promptlet.PromptletArtifactVersion}, {promptlet.PromptletArtifactDescription}, {promptlet.PromptletArtifactContent}, {promptlet.VariableStartDeliminator}, {promptlet.VariableEndDeliminator}");
            }



            var retval = csvContent.ToString();

            ExportString = retval;

            return retval;
        }

    }
}
