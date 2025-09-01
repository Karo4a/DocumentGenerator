using Xunit;

namespace DocumentGenerator.Web.Tests.Infrastructure
{
    [CollectionDefinition(nameof(ProductsCollection))]
    public class ProductsCollection : ICollectionFixture<ProductsApiFixture>
    {
    }
}
