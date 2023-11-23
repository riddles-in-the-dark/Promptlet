using Moq;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.UnitTests.RepositoryTests
{
    [TestFixture]
    internal class PromptletArtifactRepositoryTests
    {
        private PromptletArtifact promptletArtifact = PromptletArtifactGenerator.GeneratePromptletArtifacts(1).First();
        private Mock<IPromptletArtifactRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IPromptletArtifactRepository>();
            _repository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(promptletArtifact);
            _repository.Setup(x => x.Create(promptletArtifact)).ReturnsAsync(promptletArtifact);
            _repository.Setup(x => x.Update(promptletArtifact)).ReturnsAsync(promptletArtifact);
        }

        [Test]
        public async Task Delete_ShouldDeletePromptletArtifact()
        {
            await _repository.Object.Delete(promptletArtifact);
            _repository.Verify(x => x.Delete(promptletArtifact), Times.Once);
        }

        [Test]
        public async Task GetById_ShouldGetByIdPromptletArtifact()
        {
            var repositoryResponse = await _repository.Object.GetById(promptletArtifact.PromptletArtifactId);
            Assert.IsNotNull(repositoryResponse);
            Assert.AreEqual(promptletArtifact, repositoryResponse);
            _repository.Verify(x => x.GetById(promptletArtifact.PromptletArtifactId), Times.Once);
        }

        [Test]
        public async Task Create_ShouldCreatePromptletArtifact()
        {
            var repositoryResponse = await _repository.Object.Create(promptletArtifact);
            Assert.IsNotNull(repositoryResponse);
            Assert.AreEqual(promptletArtifact, repositoryResponse);
            _repository.Verify(x => x.Create(promptletArtifact), Times.Once);
        }

        [Test]
        public async Task Update_ShouldUpdatePromptletArtifact()
        {
            var repositoryResponse = await _repository.Object.Update(promptletArtifact);
            Assert.IsNotNull(repositoryResponse);
            Assert.AreEqual(promptletArtifact, repositoryResponse);
            _repository.Verify(x => x.Update(promptletArtifact), Times.Once);
        }
    }
    }