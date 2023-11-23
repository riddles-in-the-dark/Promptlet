using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Promptlet.Infrastructure.Data.DataConfigurations;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.UnitTests.DataConfigurationTests
{
    [TestFixture]
    internal class PromptletArtifactConfigurationTests
    {
        [Test]
        public void PromptletArtifactEntityTypeConfigurationHasTableNameAndPrimaryKey()
        {
            var sut = new PromptletArtifactConfiguration();

            var entityType = new EntityType(nameof(PromptletArtifact), typeof(PromptletArtifact), new Model(), false, ConfigurationSource.Convention);

            var builder = new EntityTypeBuilder<PromptletArtifact>(entityType);

            sut.Configure(builder);

            var meta = builder.Metadata;

            var properties = builder.Metadata.GetDeclaredProperties();

            var tableName = builder.Metadata.GetTableName();

            var keys = properties
                .Where(p => p.IsKey())
                .Select(p => p.GetDefaultColumnName())
                .ToList<string>();

            Assert.IsNotNull(keys);
            Assert.IsNotNull(properties);
            Assert.IsNotNull(tableName);
            Assert.Contains(nameof(PromptletArtifact.PromptletArtifactId), keys);
            Assert.AreEqual(tableName, nameof(PromptletArtifact));

        }
    }
}
