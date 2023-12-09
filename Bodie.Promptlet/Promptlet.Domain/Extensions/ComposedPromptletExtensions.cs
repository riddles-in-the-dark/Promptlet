using Promptlet.Infrastructure.Models;
using System.Text;

namespace Promptlet.Domain.Extensions
{
    public static class ComposedPromptletExtensions
    {
   
        private static string AssembledBodyParts(this ComposedPromptlet composedPromptlet)
        {
            var bodyPartsBuffer = new StringBuilder();

            foreach (var item in composedPromptlet.PromptletArtifacts.OrderBy(x => x.PromptletArtifactOrder))
            {
                bodyPartsBuffer.Append(item.PromptletArtifactContent);
            }
            return bodyPartsBuffer.ToString();
        }     


        public static string AssemblePromptWithCodeSnippetKey(this ComposedPromptlet composedPromptlet, string codeSnippetKey)
        {
            if (composedPromptlet is null)
            {
                throw new ArgumentNullException(nameof(composedPromptlet));
            }

            var reducedCodeSnippetPlaceHolderKey = $"[{Guid.NewGuid()}]";

            var promptBuilder = new StringBuilder();
            promptBuilder.AppendLine(composedPromptlet.ComposedPromptletHeader);
            promptBuilder.AppendLine(composedPromptlet.AssembledBodyParts());
            promptBuilder.AppendLine(reducedCodeSnippetPlaceHolderKey);
            promptBuilder.AppendLine(composedPromptlet.ComposedPromptletFooter);

               var multipleCodeSnippetKeyFound = promptBuilder.ToString().IndexOf(codeSnippetKey, StringComparison.Ordinal) < promptBuilder.ToString().LastIndexOf(codeSnippetKey, StringComparison.Ordinal);

            if (multipleCodeSnippetKeyFound)
            {
                promptBuilder.Replace(codeSnippetKey, "");
            }
            else
            {
                promptBuilder.Replace(reducedCodeSnippetPlaceHolderKey, "");
            }

            promptBuilder.Replace(reducedCodeSnippetPlaceHolderKey,codeSnippetKey);         

            return promptBuilder.ToString();
        }

        public static string AssemblePrompt(this ComposedPromptlet composedPromptlet) 
        {
            if (composedPromptlet is null)
            {
                throw new ArgumentNullException(nameof(composedPromptlet));
            }

            var promptBuilder = new StringBuilder();
            promptBuilder.AppendLine(composedPromptlet.ComposedPromptletHeader);
            promptBuilder.AppendLine(composedPromptlet.AssembledBodyParts());
            promptBuilder.AppendLine(composedPromptlet.ComposedPromptletFooter);

            var prompt = promptBuilder.ToString();

            return prompt.ToString();
        }

        public static Dictionary<string, string> ExtractedVariableDictionary(this ComposedPromptlet composedPromptlet, int maxVariableLength = 120)
        {
            var replaceVariableDictionary = new Dictionary<string, string>();

            foreach (var item in composedPromptlet.PromptletArtifacts.OrderBy(x => x.PromptletArtifactOrder))
            {
                var inputArray = item.PromptletArtifactContent.ToCharArray();

                var valueBuilder = new StringBuilder(maxVariableLength);

                var variableStartDelim = item.VariableStartDeliminator.ToCharArray()[0];
                var variableEndDelim = item.VariableEndDeliminator.ToCharArray()[0];

                for (int pos = 0; pos <= inputArray.Length - 1; pos++)
                {
                    if (inputArray[pos] == variableStartDelim)
                    {
                        valueBuilder.Clear();

                        for (int walk = pos; walk <= pos + maxVariableLength; walk++)
                        {
                            valueBuilder.Append(inputArray[walk]);

                            if (inputArray[walk] == variableEndDelim)
                            {
                                replaceVariableDictionary.TryAdd(valueBuilder.ToString(), $"{variableStartDelim},{variableEndDelim}");
                                valueBuilder.Clear();
                                break;
                            }
                        }
                    }
                }
            }
            return replaceVariableDictionary;
        }
    }
}
