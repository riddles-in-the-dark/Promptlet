using Bodie.Promptlet.Infrastructure.Models;

namespace Bodie.Promptlet.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(PromptletContext context)
        {

            if (context.ComposedPromplets.Any())
            {
                return;
            }
       
            var promptletArtifacts = GetPromptletArtifacts();

            var composedPromplets = GetComposedPromptlets(promptletArtifacts);

            var promptCollection = GetPromptCollections(composedPromplets);

            context.PromptCollections.AddRange(promptCollection);

            context.SaveChanges();
        }

        private static PromptCollection[] GetPromptCollections(ComposedPromptlet[] composedPromplets)
        {
            return new PromptCollection[]
            {
                new PromptCollection
                {
                    PromptCollectionName = "Composed Promptlets",
                    ComposedPromptlets = composedPromplets
                }
            };
        }

        private static PromptletArtifact[] GetPromptletArtifacts()
        {
            return new PromptletArtifact[]
            {
                new PromptletArtifact
                {
                    PromptletArtifactVersion = 1,
                    PromptletArtifactOrder = 1,
                    PromptletArtifactName = "Analyze Code Smells",
                    PromptletArtifactDescription = "To look for issues with code",
                    PromptletArtifactContent = "Analyze the given [language] code for code smells and suggest improvements: [code snippet]",
                    VariableStartDeliminator = "[",
                    VariableEndDeliminator = "]"
                },
                new PromptletArtifact
                {
                    PromptletArtifactVersion = 1,
                    PromptletArtifactOrder = 2,
                    PromptletArtifactName = "Check for logging and monitoring",
                    PromptletArtifactDescription = "Ensure observability",
                    PromptletArtifactContent = "Check the following [language] code for proper logging and monitoring practices: [code snippet]",
                    VariableStartDeliminator = "[",
                    VariableEndDeliminator = "]"
                },
                new PromptletArtifact
                {
                    PromptletArtifactVersion = 1,
                    PromptletArtifactOrder = 3,
                    PromptletArtifactName = "Check for scalibility",
                    PromptletArtifactDescription = "Ensure code can scale",
                    PromptletArtifactContent = "Review the given [language] code for potential scalability issues: [code snippet]",
                    VariableStartDeliminator = "[",
                    VariableEndDeliminator = "]"
                },
                new PromptletArtifact
                {
                    PromptletArtifactVersion = 1,
                    PromptletArtifactOrder = 4,
                    PromptletArtifactName = "Check for test coverage",
                    PromptletArtifactDescription = "Ensure code is covered",
                    PromptletArtifactContent = "Assess the test coverage of the following [language] code: [code snippet]",
                    VariableStartDeliminator = "[",
                    VariableEndDeliminator = "]"
                }
            };
        }

        private static ComposedPromptlet[] GetComposedPromptlets(PromptletArtifact[] promptletArtifacts)
        {
          return  new ComposedPromptlet[]
            {
                new ComposedPromptlet
                {
                    ComposedPromptletVersion = 1,
                    ComposedPromptletName="Code Review",
                    ComposedPromptletDescription="Perform a standard code review",
                    ComposedPromptletHeader="",
                    ComposedPromptletFooter="",
                    PromptletArtifacts=promptletArtifacts
                }
            };
        }

    }
}
