
using System.Text.Json.Serialization;

namespace Promptlet.Infrastructure.Models
{
    public partial class PromptletArtifact
    {
        public int PromptletArtifactId { get; set; }
        public int PromptletArtifactOrder { get; set; }
        public string? PromptletArtifactName { get; set; }
        public string? PromptletArtifactContent { get; set; }
        public string? VariableStartDeliminator { get; set; }
        public string? VariableEndDeliminator { get; set; }
        [JsonIgnore]
        public virtual ICollection<ComposedPromptlet>? ComposedPromptlets { get; set; }

    }
}