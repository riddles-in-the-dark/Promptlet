using Promptlet.Infrastructure.Models;

namespace Promptlet.Domain.UnitTests
{
    public static class PromptletArtifactGenerator
    {
        private static Random StaticRandom = new();
        public static PromptletArtifact[] GeneratePromptletArtifactsWithRandomizedDelimeters(int count, bool randomizedVariableDelimeters = false)
        {
            var prompts = new PromptletArtifact[count];

            for (int i = 0; i < count; i++)
            {
                var startDelim = "[";
                var endDelim = "]";
                var artifactContent = GetRandomPromptletArtifactContent();

                if (randomizedVariableDelimeters)
                {
                     endDelim = GetRandomVariableEndDeliminator();
                     artifactContent = GetRandomPromptletArtifactContent();
                    artifactContent = artifactContent
                        .Replace("[", startDelim)
                        .Replace("]", endDelim);
                }                

                prompts[i] = new PromptletArtifact
                {
                    PromptletArtifactId = StaticRandom.Next(1, 1001),
                    PromptletArtifactOrder = StaticRandom.Next(1, 1001),
                    PromptletArtifactName = GetRandomPromptletArtifactName(),
                    PromptletArtifactContent = artifactContent,
                    VariableStartDeliminator = startDelim,
                    VariableEndDeliminator = endDelim
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
            "Analyze the given [language] code for code smells and suggest improvements: [code snippet] [LinkedPromptlet(1,2)]",
            "Identify design flaws in the given [language] code and suggest improvements: [code snippet]  [LinkedPromptlet(1,2)]",
            " [LinkedPromptlet(1,2)] Detect performance bottlenecks in the given [language] code and suggest optimizations: [code snippet] "
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
}
