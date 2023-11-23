using Moq;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.UnitTests.RepositoryTests
{
    [TestFixture]
    internal class PromptletCollectionRepositoryTests
    {
        private PromptletCollection promptletCollection = PromptletCollectionGenerator.GeneratePromptletCollections(1).First();
        private List<PromptletCollection> promptletCollections = PromptletCollectionGenerator.GeneratePromptletCollections(5).ToList();
        private Mock<IPromptletCollectionRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IPromptletCollectionRepository>();
            _repository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(promptletCollection);
            _repository.Setup(x => x.GetAll()).ReturnsAsync(promptletCollections);
            _repository.Setup(x => x.Create(promptletCollection)).ReturnsAsync(promptletCollection);
            _repository.Setup(x => x.Update(promptletCollection)).ReturnsAsync(promptletCollection);
        }

        [Test]
        public async Task Delete_ShouldDeletePromptletCollection()
        {
            await _repository.Object.Delete(promptletCollection);
            _repository.Verify(x => x.Delete(promptletCollection), Times.Once);
        }

        [Test]
        public async Task Create_ShouldCreatePromptletCollection()
        {
            await _repository.Object.Create(promptletCollection);
            _repository.Verify(x => x.Create(promptletCollection), Times.Once);
        }

        [Test]
        public async Task Update_ShouldDeletePromptletCollection()
        {
            await _repository.Object.Update(promptletCollection);
            _repository.Verify(x => x.Update(promptletCollection), Times.Once);
        }

        [Test]
        public async Task GetById_ShouldGetByIdPromptletCollection()
        {
            var repositoryResponse = await _repository.Object.GetById(promptletCollection.PromptletCollectionId);
            Assert.That(repositoryResponse, Is.Not.Null);
            Assert.That(repositoryResponse, Is.EqualTo(promptletCollection));
        }

        [Test]
        public async Task GetAll_ShouldGetAllPromptletCollections()
        {
            var repositoryResponse = await _repository.Object.GetAll();
            CollectionAssert.AllItemsAreNotNull(repositoryResponse);
            CollectionAssert.AreEquivalent(promptletCollections, repositoryResponse);
        }
    }
}