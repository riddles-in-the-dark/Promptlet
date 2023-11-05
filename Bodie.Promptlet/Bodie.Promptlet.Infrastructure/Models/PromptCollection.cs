using System.ComponentModel.DataAnnotations;

namespace Bodie.Promptlet.Infrastructure.Models
{
    public class PromptCollection
    {
        [Key]
        public int PromptCollectionId { get; set; }
        [Required(ErrorMessage ="Must have Prompt Collection Name.")]
        public string PromptCollectionName { get; set; }
        public ICollection<ComposedPromptlet> ComposedPromptlets { get; set; } = new List<ComposedPromptlet>();
    }
}
