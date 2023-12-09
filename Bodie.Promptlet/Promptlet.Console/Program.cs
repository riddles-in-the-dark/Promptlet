using Microsoft.EntityFrameworkCore;
using Promptlet.Infrastructure;
using Promptlet.Infrastructure.Data;
using Promptlet.Infrastructure.Models;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;

internal class Program
{
    static async Task Main(string[] args)
    {
        var replacementPairs = new Dictionary<string, string>();
        replacementPairs.Add("language", "C#");
        replacementPairs.Add("code snippet", @"TestMe(){return 'test'}");
        using (var context = new PromptletDbContextFactory().CreateDbContext())
        {
            DbInit.Init(context);
            var promptletCollections = await context.PromptletCollections
                .Include(c => c.ComposedPromptlets)
                .ThenInclude(p => p.PromptletArtifacts)
                .Where(x => x.PromptletCollectionId == 1)
                .ToListAsync();

            foreach (var promptletCollection in promptletCollections)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(promptletCollection.PromptletCollectionName);
                foreach (var composedPromptlet in promptletCollection.ComposedPromptlets)
                {
                    var str = await AssemblePrompt(composedPromptlet, replacementPairs);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\t(id:{composedPromptlet.ComposedPromptletId}){composedPromptlet.ComposedPromptletName}");
                    foreach (var artifact in composedPromptlet.PromptletArtifacts.OrderBy(x => x.PromptletArtifactOrder))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"\t({artifact.PromptletArtifactOrder}) {artifact.PromptletArtifactName}");
                    }
                }
            }
        }

        Console.WriteLine("\r\nPress any key to continue ...");
        Console.Read();
    }

    private const string CodeSnippetPlaceHolderKey = "[aquafartbubble]";
    private const int MaxVariableLength = 120;

    public static async Task<string> AssemblePrompt(ComposedPromptlet promptlet,
                                                    IDictionary<string, string> replacementPairs)
    {
        StringBuilder
            promptBufferBuilder = new();

        bool
            promptletHasDuplicateCodeSnippetVariableKeys = false;

        HashSet<string>
            codeSnippetVariableKeyWithDelimetersCollection = new();

        Dictionary<string, Dictionary<string, string>>
            artifactContentWithExtractedVariableKeysCollection = new();

        await ProcessArtifactsAndExtractDuplicateCodeSnippetKeys(    promptlet,
                                                                     artifactContentWithExtractedVariableKeysCollection,
                                                                 ref promptletHasDuplicateCodeSnippetVariableKeys,
                                                                 ref codeSnippetVariableKeyWithDelimetersCollection
                                                                 );

        promptBufferBuilder.AppendLine(promptlet.ComposedPromptletHeader);

        /*Build out the body parts*/
        foreach (var item in artifactContentWithExtractedVariableKeysCollection)
        {
            var artifactContent = item.Key;

            if (promptletHasDuplicateCodeSnippetVariableKeys)
            {
                foreach (var codeSnippetVariableKeyWithDelimeters in codeSnippetVariableKeyWithDelimetersCollection)
                {
                    _=artifactContent.Replace(oldValue: codeSnippetVariableKeyWithDelimeters, newValue: "");
                }

                item.Value.Remove(ReservedKeywords.CodeSnippet);
            }

            foreach (var replacement in replacementPairs)
            {
                _=artifactContent.Replace(oldValue: replacement.Key, newValue: replacement.Value);
            }

            promptBufferBuilder.AppendLine(artifactContent);
        }

        var returnValue = promptBufferBuilder.ToString() ?? "";

        return returnValue;
    }

    private static Task ProcessArtifactsAndExtractDuplicateCodeSnippetKeys(
                                               ComposedPromptlet promptlet,  
                                               Dictionary<string, Dictionary<string, string>> artifactContentWithExtractedVariableKeyCollection,
                                               ref bool duplicateCodeSnippetKeys, 
                                               ref HashSet<string> codeSnippetKeysWithDelimeters                                              )
    {
        foreach (var (artifact, l_Delim, r_Delim) in from artifact in promptlet.PromptletArtifacts
                                                     let l_Delim = artifact.VariableStartDeliminator?.ToCharArray()[0]
                                                     let r_Delim = artifact.VariableEndDeliminator?.ToCharArray()[0]
                                                     select (artifact, l_Delim, r_Delim))
        {
            duplicateCodeSnippetKeys = !codeSnippetKeysWithDelimeters.Add($"{l_Delim}{ReservedKeywords.CodeSnippet}{r_Delim}")
                                       || codeSnippetKeysWithDelimeters.Count > 1
                                       || duplicateCodeSnippetKeys;

            var extractedVariableKeyCollection = ExtractVariableKeyCollection(artifact.PromptletArtifactContent.AsSpan(),
                                                                                    l_Delim, r_Delim);

            if (extractedVariableKeyCollection.Any())
            {
                artifactContentWithExtractedVariableKeyCollection.Add(artifact.PromptletArtifactContent ?? "", extractedVariableKeyCollection);
            }
        }
        return Task.CompletedTask;
    }

    public static Dictionary<string, string> ConvertCommaSeparatedStringToDictionary(string commaSeparatedString, string defaultValue = "")
    {
        var dictionary = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(commaSeparatedString))
        {
            var items = commaSeparatedString.Split(',');
            foreach (var item in items)
            {
                dictionary.TryAdd(item.Trim(), defaultValue);
            }
        }
        return dictionary;
    }


    private static Tuple<int, int> NextVariableCoordinate(ReadOnlySpan<char> inputValue, ReadOnlySpan<char> startDelim, ReadOnlySpan<char> endDelim)
    {
        var chunkStart = inputValue.IndexOf(startDelim);
        var chunkEnd = inputValue.IndexOf(endDelim);
        return Tuple.Create(chunkStart, chunkEnd);
    }

    public static Dictionary<string,string> ExtractVariableKeyCollection(ReadOnlySpan<char> input, char? startDelim, char? endDelim)
    {
        var discoveredVariables = new StringBuilder(MaxVariableLength);
        var returnVals = new Dictionary<string, string>();

        for (int pos = 0; pos <= input.Length - 1 && pos <= MaxVariableLength; pos++)
        {
            if (input[pos] == startDelim)
            {
                discoveredVariables.Clear();

                for (int walk = pos; walk <= pos + MaxVariableLength; walk++)
                {
                    discoveredVariables.Append(input[walk]);

                    if (input[walk] == endDelim)
                    {
                        var val = discoveredVariables.ToString();
                        var trimVal = val.AsSpan().Slice(1, discoveredVariables.Length - 2).ToString();
                        returnVals.Add(val, trimVal);
                        break;
                    }
                }
            }
        }
        return returnVals;
    }

    public class ComposedPromptletMergeData
    {
        public string Header { get; set; }
        public string RawBodyString { get; set; }
        public string Footer { get; set; }
        public string Variables { get; set; }
    }

    public static class ReservedKeywords
    {
        public const string CodeSnippet = "code snippet";

        public static string[] GetReserverdKeywordList(Type type)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            return fields.Where(f => f.IsLiteral && f.FieldType == typeof(string))
                .Select(f => (string)f.GetValue(null))
                .ToArray();
        }
    }

}