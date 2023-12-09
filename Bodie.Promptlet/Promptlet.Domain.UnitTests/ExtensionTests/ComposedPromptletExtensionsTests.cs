using Moq;
using Promptlet.Domain.Contracts;
using Promptlet.Domain.Services;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Models;
using Promptlet.Domain.Extensions;

namespace Promptlet.Domain.UnitTests.ExtensionTests
{
    [TestFixture]
    internal class ComposedPromptletExtensionsTests
    {
        private PromptletCollection promptletCollection = PromptletCollectionGenerator.GeneratePromptletCollections(1, false).First();
        private PromptletCollection promptletCollectionWithRandomDelims = PromptletCollectionGenerator.GeneratePromptletCollections(1, true).First();
        private List<PromptletCollection> promptletCollections = PromptletCollectionGenerator.GeneratePromptletCollections(5, false).ToList();

        private PromptletCollectionService _service;
        private Mock<IPromptletCollectionRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IPromptletCollectionRepository>();
            _repository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(promptletCollection);
            _repository.Setup(x => x.GetAll()).ReturnsAsync(promptletCollections);
            _repository.Setup(x => x.Create(promptletCollection)).ReturnsAsync(promptletCollection);
            _repository.Setup(x => x.Update(promptletCollection)).ReturnsAsync(promptletCollection);

            _service = new PromptletCollectionService(_repository.Object);
        }

        [Test]
        public async Task AssemblePromptWithCodeSnippetKey_ExpectedBehaviour()
        {
            var composedPromptlet = promptletCollection.ComposedPromptlets.FirstOrDefault();

            var prompt = composedPromptlet.AssemblePromptWithCodeSnippetKey("[code snippet]");

            Assert.IsNotNull(prompt);

        }


        [Test]
        public async Task AssemblePrompt_ExpectedBehaviour()
        {
            var composedPromptlet = promptletCollection.ComposedPromptlets.FirstOrDefault();

            var prompt = composedPromptlet.AssemblePrompt();

            Assert.IsNotNull(prompt);

        }

        [Test]
        public async Task ExtractVariableDictionary_ExpectedBehavior()
        {
            var maxVariableLength = 120;
            var composedPromptlet = promptletCollectionWithRandomDelims.ComposedPromptlets.FirstOrDefault();          

            var variableDictionary = composedPromptlet.ExtractedVariableDictionary(maxVariableLength);
            Assert.IsNotNull(variableDictionary);
        }

        [Test]
        public async Task ExtractVariableReplacementDictionary_ExpectedBehavior()
        {
            var maxVariableLength = 120;
            var composedPromptlet = promptletCollection.ComposedPromptlets.FirstOrDefault();

            var variableDictionary = composedPromptlet.ExtractedVariableDictionary(maxVariableLength);

            var replacementDictionary = new Dictionary<string,string>();

            foreach (var replacement in variableDictionary) 
            {
                var replacementKey = replacement.Key;
                replacementKey = replacementKey.Remove(0, 1);
                replacementKey = replacementKey.Remove(replacementKey.Length-1, 1);
                replacementDictionary.TryAdd(replacementKey, "");
            }

            foreach (var replacement in replacementDictionary)
            {
                replacementDictionary[replacement.Key] = Guid.NewGuid().ToString();
            }

            var prompt = composedPromptlet.AssemblePromptWithCodeSnippetKey("[code snippet]");



            Assert.IsNotNull(replacementDictionary);
        }
    }
}
