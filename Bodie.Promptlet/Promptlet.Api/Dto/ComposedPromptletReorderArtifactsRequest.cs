namespace Promptlet.Api.Dto
{
    public class ComposedPromptletReorderArtifactsRequest
    {
        public int ComposedPromptletId { get; set; }

        public Dictionary<int,int> PromptletArtifactIdNewOrder { get; set; }
    }

}
