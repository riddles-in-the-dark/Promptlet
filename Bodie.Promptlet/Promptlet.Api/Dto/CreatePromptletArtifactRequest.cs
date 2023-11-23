namespace Promptlet.Api.Dto
{
    public record CreatePromptletArtifactRequest(int PromptletArtifactOrder, string? PromptletArtifactName, string? PromptletArtifactContent, string? VariableStartDeliminator, string? VariableEndDeliminator);
}
