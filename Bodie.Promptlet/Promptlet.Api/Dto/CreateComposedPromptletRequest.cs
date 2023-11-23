namespace Promptlet.Api.Dto
{
    public record CreateComposedPromptletRequest(string? ComposedPromptletName, string? ComposedPromptletDescription, string? ComposedPromptletHeader, string? ComposedPromptletFooter);
}
