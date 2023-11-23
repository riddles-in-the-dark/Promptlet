using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Promptlet.Infrastructure.Data.DataConfigurations;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.UnitTests.DataConfigurationTests
{
    [TestFixture]
    internal class PromptletCollectionConfigurationTests
    {
        [Test]
        public void PromptletCollectionEntityTypeConfigurationHasTableNameAndPrimaryKey()
        {
            var sut = new PromptletCollectionConfiguration();

            var entityType = new EntityType(nameof(PromptletCollection), typeof(PromptletCollection), new Model(), false, ConfigurationSource.Convention);

            var builder = new EntityTypeBuilder<PromptletCollection>(entityType);

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
            Assert.Contains(nameof(PromptletCollection.PromptletCollectionId), keys);
            Assert.AreEqual(tableName, nameof(PromptletCollection));

        }
    }
}
