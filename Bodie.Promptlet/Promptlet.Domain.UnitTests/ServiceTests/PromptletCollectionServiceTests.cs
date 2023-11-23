using Moq;
using Promptlet.Domain.Contracts;
using Promptlet.Domain.Services;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Domain.UnitTests.ServiceTests
{
    [TestFixture]
    internal class PromptletCollectionServiceTests
    {
        private PromptletCollection promptletCollection = PromptletCollectionGenerator.GeneratePromptletCollections(1).First();
        private List<PromptletCollection> promptletCollections = PromptletCollectionGenerator.GeneratePromptletCollections(5).ToList();
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
        public async Task PromptletCollectionGetByIdTestAsync()
        {
            var serviceResponse = await _service.GetById(promptletCollection.PromptletCollectionId);
            
            Assert.IsNotNull(serviceResponse);
            Assert.That(serviceResponse.PromptletCollectionId, Is.EqualTo(promptletCollection.PromptletCollectionId)); 
        }

        [Test]
        public async Task PromptletCollectionGetAll()
        {
            var serviceResponse = await _service.GetAll();

            CollectionAssert.AllItemsAreNotNull(serviceResponse);
            CollectionAssert.AreEquivalent(promptletCollections, serviceResponse);
        }

        [Test]
        public async Task PromptletCollectionCreate()
        {
            var serviceResponse = await _service.Create(promptletCollection);

            Assert.NotNull(serviceResponse);
            Assert.AreEqual(serviceResponse, promptletCollection);
            _repository.Verify(x => x.Create(promptletCollection), Times.Once);
        }

        [Test]
        public async Task PromptCollectionUpdate()
        {
            var serviceResponse = await _service.Update(promptletCollection);

            Assert.NotNull(serviceResponse);
            Assert.AreEqual(serviceResponse, promptletCollection);
            _repository.Verify(x => x.Update(promptletCollection), Times.Once);
        }

        [Test]
        public async Task PromptCollectionGetDelete()
        {
            var serviceResponse = _service.Delete(promptletCollection);

            _repository.Verify(x => x.Delete(promptletCollection), Times.Once);
        }
    }

}
