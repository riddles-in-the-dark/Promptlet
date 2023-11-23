using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.UnitTests.ModelTests
{
    [TestFixture]
    internal class ComposedPromptletModelTest
    {
        [Test]
        [TestCase(1, "ComposedPromptletNameValue", "ComposedPromptletDescriptionValue", "ComposedPromptletHeaderValue", "ComposedPromptletFooterValue")]
        public void ComposedPromptletModelContainsExpectedProperties(int id, string name, string description, string header, string footer) 
        {
            var sut = new ComposedPromptlet 
            { 
                ComposedPromptletId=id, 
                ComposedPromptletName=name, 
                ComposedPromptletDescription=description, 
                ComposedPromptletHeader=header, 
                ComposedPromptletFooter=footer 
            };

            Assert.That(sut.ComposedPromptletId, Is.EqualTo(id));
            Assert.That(sut.ComposedPromptletName, Is.EqualTo(name));
            Assert.That(sut.ComposedPromptletDescription, Is.EqualTo(description));
            Assert.That(sut.ComposedPromptletHeader, Is.EqualTo(header));
            Assert.That(sut.ComposedPromptletFooter, Is.EqualTo(footer));
        }
    }
}
