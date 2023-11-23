using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.UnitTests
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
    public static class PromptletArtifactGenerator
    {
        private static Random StaticRandom = new();

        public static PromptletArtifact[] GeneratePromptletArtifacts(int count)
        {
            var prompts = new PromptletArtifact[count];

            for (int i = 0; i < count; i++)
            {
                prompts[i] = new PromptletArtifact
                {
                    PromptletArtifactId = StaticRandom.Next(1, 1001),
                    PromptletArtifactOrder = StaticRandom.Next(1, 1001),
                    PromptletArtifactName = GetRandomPromptletArtifactName(),
                    PromptletArtifactContent = GetRandomPromptletArtifactContent(),
                    VariableStartDeliminator = GetRandomVariableStartDeliminator(),
                    VariableEndDeliminator = GetRandomVariableEndDeliminator()
                };
            }

            return prompts;
        }

        private static string GetRandomPromptletArtifactName()
        {
            var names = new string[] { "Analyze Code Smells", "Identify Design Flaws", "Detect Performance Bottlenecks" };
            return names[StaticRandom.Next(names.Length)];
        }

        private static string GetRandomPromptletArtifactContent()
        {
            var contents = new string[] 
            {
            "Analyze the given [language] code for code smells and suggest improvements: [code snippet]",
            "Identify design flaws in the given [language] code and suggest improvements: [code snippet]",
            "Detect performance bottlenecks in the given [language] code and suggest optimizations: [code snippet]"
            };
            return contents[StaticRandom.Next(contents.Length)];
        }

        private static string GetRandomVariableStartDeliminator()
        {
            var delimiters = new string[] { "[", "{", "<" };
            return delimiters[StaticRandom.Next(delimiters.Length)];
        }

        private static string GetRandomVariableEndDeliminator()
        {
            var delimiters = new string[] { "]", "}", ">" };
            return delimiters[StaticRandom.Next(delimiters.Length)];
        }
    }

    public static class PromptletCollectionGenerator
    {
        private static Random StaticRandom = new();
        public static PromptletCollection[] GeneratePromptletCollections(int count)
        {
            var promptletCollections = new PromptletCollection[count];

            for (int i = 0; i < count; i++)
            {
                promptletCollections[i] = new PromptletCollection
                {
                    PromptletCollectionId = StaticRandom.Next(1, 1001),
                    PromptletCollectionName = GetRandomPromptletCollectionName(),
                    ComposedPromptlets = GetRandomComposedPromptlets()
                };
            }

            return promptletCollections;
        }

        private static string GetRandomPromptletCollectionName()
        {
            var names = new string[] { "Composed Promptlets", "Predefined Promptlets", "Custom Promptlets" };
            return names[StaticRandom.Next(names.Length)];
        }

        private static ComposedPromptlet[] GetRandomComposedPromptlets()
        {
            var composedPromptletCount = StaticRandom.Next(1, 6);
            var promptletArtifactPerComposedPromptletCount = StaticRandom.Next(1, 6);

            var composedPromptlets = ComposedPromptletGenerator.GenerateComposedPromptlets(composedPromptletCount, promptletArtifactPerComposedPromptletCount);

            return composedPromptlets;
        }
    }
}
