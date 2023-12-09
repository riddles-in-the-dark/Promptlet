using System.Text.Json.Serialization;

namespace Promptlet.Api.Dto
{
    public partial record ComposedPromptletResponse
    {
        public int ComposedPromptletId { get; set; }
        public string? ComposedPromptletName { get; set; }
        public string? ComposedPromptletDescription { get; set; }
        public string? ComposedPromptletHeader { get; set; }
        public string? ComposedPromptletFooter { get; set; }
        public virtual ICollection<PromptletArtifactResponse>? PromptletArtifacts { get; set; }
    }

    public partial record PromptletCollectionsResponse {
        public PromptletCollectionResponse[]? PromptletCollections { get; set;}
    }

}
