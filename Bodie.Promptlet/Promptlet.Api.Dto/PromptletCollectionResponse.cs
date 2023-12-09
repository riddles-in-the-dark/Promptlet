namespace Promptlet.Api.Dto
{
    public partial record PromptletCollectionResponse
    {
        public int PromptletCollectionId { get; set; }
        public string? PromptletCollectionName { get; set; }
        public virtual ICollection<ComposedPromptletResponse>? ComposedPromptlets { get; set; }

    }

}
