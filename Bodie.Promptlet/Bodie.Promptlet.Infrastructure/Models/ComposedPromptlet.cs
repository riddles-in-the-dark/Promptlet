using System.ComponentModel.DataAnnotations;

namespace Bodie.Promptlet.Infrastructure.Models
{
    public class ComposedPromptlet
    {
        [Key]
        public int ComposedPromptletId { get; set; }
        public int ComposedPromptletVersion { get; set; }

        [Required(ErrorMessage = "Enter Composed Promptlet Name")]
        public string ComposedPromptletName { get; set; }
        public string? ComposedPromptletDescription { get; set; }
        public string? ComposedPromptletHeader { get; set; }
        public string? ComposedPromptletFooter { get; set; }
        public ICollection<PromptletArtifact> PromptletArtifacts { get; set; } = new List<PromptletArtifact>();
    }
}
