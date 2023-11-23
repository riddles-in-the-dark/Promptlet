using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.UnitTests.ModelTests
{
    [TestFixture]
    internal class PromptletCollectionModelTest
    {
        [Test]
        [TestCase(1, "PromptletCollectionNameValue")]
        public void PromptletCollectionModelContainsExpectedProperties(int id, string name)
        {
            var sut = new PromptletCollection
            {
                PromptletCollectionId = id,
                PromptletCollectionName = name
            };

            Assert.That(sut.PromptletCollectionId, Is.EqualTo(id));
            Assert.That(sut.PromptletCollectionName, Is.EqualTo(name));
        }
    }



}
