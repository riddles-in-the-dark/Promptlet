using Promptlet.Infrastructure.Models;
using System.Runtime.CompilerServices;
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

        public static VariableCollection ExtractedVariableDictionary(this ComposedPromptlet composedPromptlet, int maxVariableLength = 120)
        {
            var variableCollection = new VariableCollection();

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

                        var id = Guid.NewGuid().ToString();

                        for (int walk = pos; walk <= pos + maxVariableLength; walk++)
                        {                           
                            valueBuilder.Append(inputArray[walk]);

                                if (inputArray[walk] == variableEndDelim)
                                {
                                    var discoveredVariable = valueBuilder.ToString();
                                    var discoveredVariableIsNested = DiscoveredVariableIsNested(discoveredVariable);
                                        if (discoveredVariableIsNested)
                                        {
                                            var composedPromptletValues = discoveredVariable.Substring(2, discoveredVariable.Length - 4).Split(',');
                                            foreach (var value in composedPromptletValues) 
                                            {
                                                variableCollection.NestedVariables.TryAdd(value, id);
                                            }
                                        }
                                        else
                                        {
                                            variableCollection.StringReplacementVariables.TryAdd(valueBuilder.ToString(), $"{id},{variableStartDelim},{variableEndDelim}");
                                        }
                                    valueBuilder.Clear();
                                    break;
                                }
                            }
                    }
                }
            }
            return variableCollection;
        }

        private static bool DiscoveredVariableIsNested(string value) 
        {
            var nestedVariableDelimeterPos = value.IndexOf("(");
            var nestedVariableDescription = value.IndexOf("(") > 0 ? value.Substring(0, nestedVariableDelimeterPos) : "StringReplacement";

            Enum.TryParse<VariableReplacementProviderType>(nestedVariableDescription, out var replacementType);

            IVariableReplacementProvider replacementProvider = Utilities.GetVariableReplacementProvider(replacementType);
            
            var trimDelimFromValue = value.Substring(1, value.Length-2);

            return trimDelimFromValue.First().Equals('(') && trimDelimFromValue.Last().Equals(')');
        }

        private static string[] NonStringReplacementVariableTypes = new string[] { "LinkedPromptlet" };

    }

    public interface IVariableReplacementProvider 
    {
        public VariableReplacementProviderType VariableReplacementProviderType { get; init; }
        public string GetVariableReplacement(string[] args);
    }

    public class LinkedPromptletProvider : IVariableReplacementProvider
    {
        public VariableReplacementProviderType VariableReplacementProviderType { get; init; } = VariableReplacementProviderType.LinkedPromptlet;
        public string GetVariableReplacement(string[] args)
        {
            throw new NotImplementedException();
        }
    }

    public class StringReplacementProvider : IVariableReplacementProvider
    {
        public VariableReplacementProviderType VariableReplacementProviderType { get; init; } = VariableReplacementProviderType.StringReplacement;
        public string GetVariableReplacement(string[] args)
        {
            throw new NotImplementedException();
        }
    }

    public static class Utilities
    {
        public static IVariableReplacementProvider GetVariableReplacementProvider(VariableReplacementProviderType typeName)
        {
            return typeName switch
            {
                VariableReplacementProviderType.StringReplacement => new StringReplacementProvider(),
                VariableReplacementProviderType.LinkedPromptlet => new LinkedPromptletProvider(),
                _ => new StringReplacementProvider(),
            };
        }
        public static T GetInstance<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(
                type: typeof(T), args: args);
        }
    }


    [InterpolatedStringHandler]
    public ref struct PromptletInterpolatedStringHandler
    {
        private StringBuilder builder;
        private readonly int _length = 0;
        private readonly int _formattedCount = 0;
        private readonly string[] _args;

        public PromptletInterpolatedStringHandler(int length, int formattedCount, string[]? args)
        {
            _length = length;
            _formattedCount = formattedCount;
            builder = new StringBuilder(length);
            _args = args ?? Array.Empty<string>();
        }

        internal string GetFormattedText() => builder.ToString();

        public void AppendLiteral(string s) => builder.Append(s);

        public void AppendFormatted<T>(T t) => builder.Append(t?.ToString());

        public void AppendFormatted<T>(T t, string format) where T : IFormattable
        {
            builder.Append(t?.ToString(format, null));
        }

    }

    public enum VariableReplacementProviderType
    {
        StringReplacement = 0,
        LinkedPromptlet = 1
    }
}
