using Bodie.Promptlet.Web.DTO;
using System.Text;

namespace Bodie.Promptlet.Web.Utilities
{
    public static class PromptAssembler
    {
        private const string CodeSnippetKey = "[code snippet]";
        private const string CodeSnippetPlaceHolderKey = "[aquafartbubble]";
        private const int MaxVariableLength = 120;

        public static string AssemblePrompt(PreviewPromptlet promptParts, IDictionary<string, string> replacementPairs)
        {

            var bufferBuilder = new StringBuilder();
            bufferBuilder.AppendLine(promptParts.ComposedPromptletHeader);
            bufferBuilder.AppendLine(promptParts.RawBodyString);
            bufferBuilder.AppendLine(CodeSnippetPlaceHolderKey);
            bufferBuilder.AppendLine(promptParts.ComposedPromptletFooter);

            var multiSnippet = bufferBuilder.ToString().IndexOf(CodeSnippetKey, StringComparison.Ordinal) < bufferBuilder.ToString().LastIndexOf(CodeSnippetKey, StringComparison.Ordinal);

            if (multiSnippet)
            {
                bufferBuilder.Replace(CodeSnippetKey, "");
                bufferBuilder.Replace(CodeSnippetPlaceHolderKey, $"\r{replacementPairs[CodeSnippetKey]}\r");
            }
            else
            {
                bufferBuilder.Replace(CodeSnippetPlaceHolderKey, "");
            }

            foreach (var replacement in replacementPairs)
            {
                bufferBuilder.Replace(replacement.Key, replacement.Value);
            }

            var returnValue = bufferBuilder.ToString() ?? "";

            return returnValue;
        }



        //public static string AsseembleRawPromptFromParts(PromptParts promptParts)
        //{
        //    var bufferBuilder = new StringBuilder();
        //    bufferBuilder.AppendLine(promptParts.Header);
        //    bufferBuilder.AppendLine(promptParts.Body);
        //    bufferBuilder.AppendLine(promptParts.Footer);
        //    return bufferBuilder.ToString();
        //}
        //public static Dictionary<string, Tuple<int, int>> ParseElementsFromParts(PromptParts promptParts, char startDelim, char endDelim)
        //{
        //    return ExtractVariableNamesAndOrdinalAndCoordinates(AsseembleRawPromptFromParts(promptParts), startDelim, endDelim);
        //}

        //public static Dictionary<string, string> ExtractVariableDictionary(PromptParts promptParts, char startDelim, char endDelim)
        //{
        //    return ExtractVariableDictionary(AsseembleRawPromptFromParts(promptParts), startDelim, endDelim);
        //}

    }
}
