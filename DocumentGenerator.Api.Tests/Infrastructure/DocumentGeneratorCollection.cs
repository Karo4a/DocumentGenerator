using Xunit;

namespace DocumentGenerator.Api.Tests.Infrastructure;

[CollectionDefinition(nameof(DocumentGeneratorCollection))]
public class DocumentGeneratorCollection : ICollectionFixture<DocumentGeneratorApiFixture>
{
}
