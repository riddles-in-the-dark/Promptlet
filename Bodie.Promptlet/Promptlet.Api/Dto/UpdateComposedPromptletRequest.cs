namespace Promptlet.Api.Dto
{
    public record UpdateComposedPromptletRequest(int ComposedPromptletId, string? ComposedPromptletName, string? ComposedPromptletDescription, string? ComposedPromptletHeader, string? ComposedPromptletFooter);
}
