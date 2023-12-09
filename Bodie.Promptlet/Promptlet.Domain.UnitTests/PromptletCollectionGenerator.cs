using Promptlet.Infrastructure.Models;

namespace Promptlet.Domain.UnitTests
{
    public static class PromptletCollectionGenerator
    {
        private static Random StaticRandom = new();
        public static PromptletCollection[] GeneratePromptletCollections(int count, bool randomizedVariableDelimeters)
        {
            var promptletCollections = new PromptletCollection[count];

            for (int i = 0; i < count; i++)
            {
                promptletCollections[i] = new PromptletCollection
                {
                    PromptletCollectionId = StaticRandom.Next(1, 1001),
                    PromptletCollectionName = GetRandomPromptletCollectionName(),
                    ComposedPromptlets = GetRandomComposedPromptlets(randomizedVariableDelimeters)
                };
            }

            return promptletCollections;
        }

        private static string GetRandomPromptletCollectionName()
        {
            var names = new string[] { "Composed Promptlets", "Predefined Promptlets", "Custom Promptlets" };
            return names[StaticRandom.Next(names.Length)];
        }

        private static ComposedPromptlet[] GetRandomComposedPromptlets(bool randomizedVariableDelimeters=false)
        {
            var composedPromptletCount = StaticRandom.Next(1, 6);
            var promptletArtifactPerComposedPromptletCount = StaticRandom.Next(1, 6);

            var composedPromptlets = ComposedPromptletGenerator.GenerateComposedPromptlets(composedPromptletCount, promptletArtifactPerComposedPromptletCount, randomizedVariableDelimeters);

            return composedPromptlets;
        }
    }
}
