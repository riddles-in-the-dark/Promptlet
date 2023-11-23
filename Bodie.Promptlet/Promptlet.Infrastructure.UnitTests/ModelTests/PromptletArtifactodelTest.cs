using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.UnitTests.ModelTests
{
    [TestFixture]
    internal class PromptletArtifactodelTest
    {
        [Test]
        [TestCase(1, "PromptletArtifactNameValue", 1, "PromptletArtifactContentValue")]
        public void PromptletArtifactModelContainsExpectedProperties(int id, string name, int order, string content)
        {
            var sut = new PromptletArtifact
            {
                PromptletArtifactId = id,
                PromptletArtifactName = name,
                PromptletArtifactOrder = order,
                PromptletArtifactContent = content
            };

            Assert.That(sut.PromptletArtifactId, Is.EqualTo(id));
            Assert.That(sut.PromptletArtifactName, Is.EqualTo(name));
            Assert.That(sut.PromptletArtifactOrder, Is.EqualTo(order));
            Assert.That(sut.PromptletArtifactContent, Is.EqualTo(content));
        }
    }



}
