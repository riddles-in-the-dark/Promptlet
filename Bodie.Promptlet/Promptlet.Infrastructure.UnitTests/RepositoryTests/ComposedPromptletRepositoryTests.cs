using Moq;
using Promptlet.Infrastructure.Contracts;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.UnitTests.RepositoryTests
{
    [TestFixture]
    internal class ComposedPromptletRepositoryTests
    {
        private ComposedPromptlet composedPromptlet = ComposedPromptletGenerator.GenerateComposedPromptlets(1, 3).First();
        private Mock<IComposedPromptletRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IComposedPromptletRepository>();
            _repository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(composedPromptlet);
            _repository.Setup(x => x.Create(composedPromptlet)).ReturnsAsync(composedPromptlet);
            _repository.Setup(x => x.Update(composedPromptlet)).ReturnsAsync(composedPromptlet);
        }

        [Test]
        public async Task Delete_ShouldDeleteComposedPromptlet()
        {
            await _repository.Object.Delete(composedPromptlet);
            _repository.Verify(x => x.Delete(composedPromptlet), Times.Once);
        }

        [Test]
        public async Task GetById_ShouldGetByIdComposedPromptlet()
        {
             var repositoryResponse = await _repository.Object.GetById(composedPromptlet.ComposedPromptletId);
            Assert.IsNotNull(repositoryResponse);
            Assert.AreEqual(composedPromptlet, repositoryResponse);
            _repository.Verify(x => x.GetById(composedPromptlet.ComposedPromptletId), Times.Once);
        }

        [Test]
        public async Task Create_ShouldCreateComposedPromptlet()
        {
            var repositoryResponse = await _repository.Object.Create(composedPromptlet);
            Assert.IsNotNull(repositoryResponse);
            Assert.AreEqual(composedPromptlet, repositoryResponse);
            _repository.Verify(x => x.Create(composedPromptlet), Times.Once);
        }

        [Test]
        public async Task Update_ShouldUpdateComposedPromptlet()
        {
            var repositoryResponse = await _repository.Object.Update(composedPromptlet);
            Assert.IsNotNull(repositoryResponse);
            Assert.AreEqual(composedPromptlet, repositoryResponse);
            _repository.Verify(x => x.Update(composedPromptlet), Times.Once);
        }

        [Test]
        public async Task ReorderPromptletArtifacts_ShouldReorderPromptletArtifacts()
        {
            var promptletArtifactsNewOrdinal = new Dictionary<int, int>();

            var promptletArtifactsOriginalOrdinal = new Dictionary<int, int>();

            var expectedComposedPromptlet = new ComposedPromptlet
            {
                ComposedPromptletId = composedPromptlet.ComposedPromptletId,
                ComposedPromptletName = composedPromptlet.ComposedPromptletName,
                ComposedPromptletHeader = composedPromptlet.ComposedPromptletHeader,
                ComposedPromptletDescription = composedPromptlet.ComposedPromptletDescription,
                ComposedPromptletFooter = composedPromptlet.ComposedPromptletFooter,
                PromptletArtifacts = new List<PromptletArtifact>()
            };

            var newOrder = 0;

            foreach (var artifact in composedPromptlet.PromptletArtifacts.OrderBy(x => x.PromptletArtifactOrder))
            {
                promptletArtifactsOriginalOrdinal.Add(artifact.PromptletArtifactId, artifact.PromptletArtifactOrder);

                promptletArtifactsNewOrdinal.Add(artifact.PromptletArtifactId, ++newOrder);

                expectedComposedPromptlet.PromptletArtifacts.Add(new PromptletArtifact
                {
                    PromptletArtifactId = artifact.PromptletArtifactId,
                    PromptletArtifactName = artifact.PromptletArtifactName,
                    PromptletArtifactOrder = newOrder,
                    PromptletArtifactContent = artifact.PromptletArtifactContent,
                    VariableStartDeliminator = artifact.VariableStartDeliminator,
                    VariableEndDeliminator = artifact.VariableEndDeliminator
                });
            }

            _repository.Setup(x=>x.ReorderPromptletArtifacts(composedPromptlet.ComposedPromptletId,promptletArtifactsNewOrdinal)).ReturnsAsync(expectedComposedPromptlet);

            var reorderedComposedPromptlet = await _repository.Object.ReorderPromptletArtifacts(composedPromptlet.ComposedPromptletId, promptletArtifactsNewOrdinal);

            foreach (var originalArtifact in composedPromptlet.PromptletArtifacts )
            {
                var testArtifact = reorderedComposedPromptlet.PromptletArtifacts.First(x => x.PromptletArtifactId == originalArtifact.PromptletArtifactId);
                Assert.IsNotNull(testArtifact);
                Assert.That(testArtifact.PromptletArtifactOrder, !Is.EqualTo(originalArtifact.PromptletArtifactOrder));
            }

            Assert.NotNull(reorderedComposedPromptlet);
            Assert.AreEqual(3, reorderedComposedPromptlet.PromptletArtifacts.Count());
            Assert.AreEqual(1, reorderedComposedPromptlet.PromptletArtifacts.OrderBy(x => x.PromptletArtifactOrder).First().PromptletArtifactOrder);
            Assert.AreEqual(2, reorderedComposedPromptlet.PromptletArtifacts.OrderBy(x => x.PromptletArtifactOrder).Skip(1).First().PromptletArtifactOrder);
            Assert.AreEqual(3, reorderedComposedPromptlet.PromptletArtifacts.OrderBy(x => x.PromptletArtifactOrder).Skip(2).First().PromptletArtifactOrder);

        }
    }
    }