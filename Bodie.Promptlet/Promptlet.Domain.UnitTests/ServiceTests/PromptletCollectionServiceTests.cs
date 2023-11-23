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
        [Test]
        public async Task PromptletCollectionGetByIdTestAsync()
        {
            
            var mockPromptletCollectionRepository = new Mock<IPromptletCollectionRepository>();

            var promptletCollectionDomainService = new PromptletCollectionService(mockPromptletCollectionRepository.Object);

            var expectedPromptCollection = PromptletCollectionGenerator.GeneratePromptletCollections(1).First();

            mockPromptletCollectionRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(expectedPromptCollection);

            var serviceResponse = await promptletCollectionDomainService.GetById(expectedPromptCollection.PromptletCollectionId);

           Assert.That(serviceResponse.PromptletCollectionId, Is.EqualTo(expectedPromptCollection.PromptletCollectionId));
 
        }

        [Test]
        public async Task PromptletCollectionGetAll()
        {

            var mockPromptletCollectionRepository = new Mock<IPromptletCollectionRepository>();
            var promptletCollectionDomainService = new PromptletCollectionService(mockPromptletCollectionRepository.Object);

            var expectedPromptCollection = PromptletCollectionGenerator.GeneratePromptletCollections(5).ToList();

            mockPromptletCollectionRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(expectedPromptCollection);

            var serviceResponse = await promptletCollectionDomainService.GetAll();

            CollectionAssert.AreEquivalent(expectedPromptCollection, serviceResponse);
        }

        [Test]
        public async Task PromptletCollectionCreate()
        {
            //Arrange - mock the promptlet collection repository
            var mockPromptletCollectionRepository = new Mock<IPromptletCollectionRepository>();

            //Arrange - get a test promptlet collection
            var expectedPromptCollection = PromptletCollectionGenerator.GeneratePromptletCollections(1).First();

            //Arrange - setup the mock promptlet collection repository to return the expectedPromptCollection
            //when Create is called with the expectedPromptCollection as a parameter
            mockPromptletCollectionRepository
                        .Setup(x => x.Create(expectedPromptCollection))
                        .ReturnsAsync(expectedPromptCollection);

            //Arrange - initialize a promptlet collection domain service instance with the mocked promptlet collection repository
            var promptletCollectionDomainService = new PromptletCollectionService(mockPromptletCollectionRepository.Object);
      
            // Act
            var createdPromptletCollection = await promptletCollectionDomainService.Create(expectedPromptCollection);

            // Assert
            Assert.NotNull(createdPromptletCollection);
            Assert.AreEqual(expectedPromptCollection, createdPromptletCollection);
            mockPromptletCollectionRepository.Verify(x => x.Create(expectedPromptCollection), Times.Once);
        }

        [Test]
        public async Task PromptCollectionUpdate()
        {

        }
        [Test]
        public async Task PromptCollectionGetDelete()
        {

        }
    }

}
