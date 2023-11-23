using Promptlet.Infrastructure.Models;
using System.Diagnostics.CodeAnalysis;

namespace Promptlet.Infrastructure.Data
{
    [ExcludeFromCodeCoverage]
    public static class DbInit
    {
        public static void Init(PromptletDbContext dbContext)
        {
            if (dbContext.PromptletCollections.Any()) { return; }

            var promptletArtifacts = GetPromptletArtifacts();
            var composedPromptlets = GetComposedPromptlets(promptletArtifacts);
            var promptletCollection = GetPromptletCollections(composedPromptlets);
            dbContext.AddRange(promptletCollection);
            dbContext.SaveChanges();
        }

        private static PromptletCollection[] GetPromptletCollections(ComposedPromptlet[] composedPromptlets)
        {
            return new PromptletCollection[]
            {
                new PromptletCollection
                {
                    PromptletCollectionName = "Programming",
                    ComposedPromptlets = composedPromptlets
                }
            };
        }

        private static ComposedPromptlet[] GetComposedPromptlets(PromptletArtifact[] promptletArtifacts)
        {
            return new ComposedPromptlet[]
           {
                new ComposedPromptlet
                {
                    ComposedPromptletName="Code Review",
                    ComposedPromptletDescription="Perform a standard code review",
                    ComposedPromptletHeader="",
                    ComposedPromptletFooter="",
                    PromptletArtifacts=promptletArtifacts
                }
           };
        }

        private static PromptletArtifact[] GetPromptletArtifacts()
        {
            return new PromptletArtifact[]
          {
                new PromptletArtifact
                {
                    PromptletArtifactOrder = 1,
                    PromptletArtifactName = "Analyze Code Smells",
                    PromptletArtifactContent = "Analyze the given [language] code for code smells and suggest improvements: [code snippet]",
                    VariableStartDeliminator = "[",
                    VariableEndDeliminator = "]"
                },
                new PromptletArtifact
                {
                    PromptletArtifactOrder = 2,
                    PromptletArtifactName = "Check for logging and monitoring",
                    PromptletArtifactContent = "Check the following [language] code for proper logging and monitoring practices: [code snippet]",
                    VariableStartDeliminator = "[",
                    VariableEndDeliminator = "]"
                },
                new PromptletArtifact
                {
                    PromptletArtifactOrder = 3,
                    PromptletArtifactName = "Check for scalibility",
                    PromptletArtifactContent = "Review the given [language] code for potential scalability issues: [code snippet]",
                    VariableStartDeliminator = "[",
                    VariableEndDeliminator = "]"
                },
                new PromptletArtifact
                {
                    PromptletArtifactOrder = 4,
                    PromptletArtifactName = "Check for test coverage",
                    PromptletArtifactContent = "Assess the test coverage of the following [language] code: [code snippet]",
                    VariableStartDeliminator = "[",
                    VariableEndDeliminator = "]"
                }
          };
        }
    }
}
