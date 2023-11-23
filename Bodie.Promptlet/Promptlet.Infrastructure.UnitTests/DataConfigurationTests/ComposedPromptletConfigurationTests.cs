using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Promptlet.Infrastructure.Data.DataConfigurations;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Infrastructure.UnitTests.DataConfigurationTests
{
    [TestFixture]
    internal class ComposedPromptletConfigurationTests
        {
            [Test]
            public void ComposedPromptletEntityTypeConfigurationHasTableNameAndPrimaryKey()
            {
                var sut = new ComposedPromptletConfiguration();

                var entityType = new EntityType(nameof(ComposedPromptlet), typeof(ComposedPromptlet), new Model(), false, ConfigurationSource.Convention);

                var builder = new EntityTypeBuilder<ComposedPromptlet>(entityType);

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
                Assert.Contains(nameof(ComposedPromptlet.ComposedPromptletId), keys);
                Assert.AreEqual(tableName, nameof(ComposedPromptlet));

            }
        }
}
