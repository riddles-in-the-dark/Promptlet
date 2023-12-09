using System.Text.Json.Serialization;

namespace Promptlet.Api.Dto
{
    public partial class PromptletArtifactResponse
    {
        public int PromptletArtifactId { get; set; }
        public int PromptletArtifactOrder { get; set; }
        public string? PromptletArtifactName { get; set; }
        public string? PromptletArtifactContent { get; set; }
        public string? VariableStartDeliminator { get; set; }
        public string? VariableEndDeliminator { get; set; }
    }

}
