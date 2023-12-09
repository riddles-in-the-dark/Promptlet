namespace Promptlet.Api.Dto
{
    public record UpdatePromptletArtifactRequest(int PromptletArtifactId, int PromptletArtifactOrder, string? PromptletArtifactName, string? PromptletArtifactContent, string? VariableStartDeliminator, string? VariableEndDeliminator);
}
