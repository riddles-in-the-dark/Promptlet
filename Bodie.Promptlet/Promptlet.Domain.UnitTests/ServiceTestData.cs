using Promptlet.Infrastructure.Models;

namespace Promptlet.Domain.UnitTests
{
    public static class ServiceTestData
        {

            private static Random StaticRandom = new();
            internal static PromptletCollection GetPromptletCollection()
            {
                Enumerable.Range(1, 50).OrderBy(_ => StaticRandom.Next());
                return new PromptletCollection() { PromptletCollectionId= StaticRandom.Next(), PromptletCollectionName=Guid.NewGuid().ToString()};
            }

            internal static object GetPromptletCollections()
            {
            Enumerable.Range(1, 50).OrderBy(_ => StaticRandom.Next());
            return new PromptletCollection() { PromptletCollectionId = StaticRandom.Next(), PromptletCollectionName = Guid.NewGuid().ToString() };
            }

            internal static PromptletCollection[] GetPromptCollections(ComposedPromptlet[] composedPromplets)
            {
                Enumerable.Range(1, 50).OrderBy(_ => StaticRandom.Next());
                return new PromptletCollection[]
                {
                    new PromptletCollection
                    {
                        PromptletCollectionId = 1,
                        PromptletCollectionName = "Composed Promptlets",
                        ComposedPromptlets = composedPromplets
                    }
                };
            }

        internal static PromptletArtifact[] GetPromptletArtifacts(int quantity)
        {

            var promptletArtifacts = new PromptletArtifact[quantity];

            for (int i = quantity - 1; i >= 0; i--)
            {
                var promptletArtifact = new PromptletArtifact
                {
                    PromptletArtifactId = 1,
                    PromptletArtifactOrder = 1,
                    PromptletArtifactName = "Analyze Code Smells",
                    PromptletArtifactContent = "Analyze the given [language] code for code smells and suggest improvements: [code snippet]",
                    VariableStartDeliminator = "[",
                    VariableEndDeliminator = "]"
                };
            }


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

        internal static ComposedPromptlet[] GetComposedPromptlets(PromptletArtifact[] promptletArtifacts)
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
    }
}
