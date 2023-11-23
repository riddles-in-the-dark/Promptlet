using System.Text.Json.Serialization;

namespace Promptlet.Infrastructure.Models
{
    public partial class ComposedPromptlet
    {
        public int ComposedPromptletId { get; set; }
        public string? ComposedPromptletName { get; set; }
        public string? ComposedPromptletDescription { get; set; }
        public string? ComposedPromptletHeader { get; set; }
        public string? ComposedPromptletFooter { get; set; }
        public virtual ICollection<PromptletArtifact>? PromptletArtifacts { get; set; }
        [JsonIgnore]
        public virtual ICollection<PromptletCollection>? PromptletCollections { get; set; }

    }
}