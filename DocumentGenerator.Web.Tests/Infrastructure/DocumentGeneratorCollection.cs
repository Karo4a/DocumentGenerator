using Xunit;

namespace DocumentGenerator.Web.Tests.Infrastructure
{
    [CollectionDefinition(nameof(DocumentGeneratorCollection))]
    public class DocumentGeneratorCollection : ICollectionFixture<DocumentGeneratorApiFixture>
    {
    }
}
