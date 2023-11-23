namespace Promptlet.Infrastructure.Models
{
    public partial class PromptletCollection
    {
        public int PromptletCollectionId { get; set; }
        public string? PromptletCollectionName { get; set; }
        public virtual ICollection<ComposedPromptlet>? ComposedPromptlets { get; set; }

    }
}
 