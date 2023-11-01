using System.ComponentModel.DataAnnotations;

namespace Bodie.Promptlet.Infrastructure.Models
{
    public class PromptletArtifact
    {
        [Key]
        public int PromptletArtifactId { get; set; }
        public int PromptletArtifactVersion { get; set; }
        public int PromptletArtifactOrder { get; set; }

        [Required(ErrorMessage = "Enter Promptlet Artifact Name")]
        public string PromptletArtifactName { get; set; }
        public string? PromptletArtifactDescription { get; set; }
        public string? PromptletArtifactContent { get; set; }
        public string? VariableStartDeliminator { get; set; }
        public string? VariableEndDeliminator { get; set; }
        public ICollection<ComposedPromptlet> ComposedPromptlets { get; set; } = new List<ComposedPromptlet>();
    }
}
