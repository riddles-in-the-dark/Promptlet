namespace Bodie.Promptlet.Web.Pages.ImportExport
{
    public record CsvModel
    {
       public string PromptCollectionName{get; set;}

        public string ComposedPromptletName{get; set;}

        public string ComposedPromptletVersion{get; set;}

        public string ComposedPromptletDescription{get; set;}

        public string ComposedPromptletHeader{get; set;}

        public string ComposedPromptletFooter{get; set;}

        public string PromptletArtifactOrder{get; set;}

        public string PromptletArtifactName{get; set;}

        public string PromptletArtifactVersion{get; set;}

        public string PromptletArtifactDescription{get; set;}

        public string PromptletArtifactContent{get; set;}

        public string VariableStartDeliminator{get; set;}

        public string VariableEndDeliminator { get; set; }
    }
}
