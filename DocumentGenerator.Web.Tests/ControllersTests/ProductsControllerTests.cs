using Ahatornn.TestGenerator;
using DocumentGenerator.Context;
using DocumentGenerator.Web.Tests.Client;
using DocumentGenerator.Web.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace DocumentGenerator.Web.Tests.ControllersTests
{
    /// <summary>
    /// Интеграционные тесты контроллера товаров
    /// </summary>
    [Collection(nameof(ProductsCollection))]
    public class ProductsControllerTests
    {
        private readonly IDocumentGeneratorApiClient webClient;
        private readonly DocumentGeneratorContext context;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductsControllerTests(ProductsApiFixture fixture)
        {
            webClient = fixture.WebClient;
            context = fixture.Context;
        }

        /// <summary>
        /// Возвращает все значения
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            // Arrange
            var product = TestEntityProvider.Shared.Create<Entities.Product>();
            await context.AddAsync(product);
            await context.SaveChangesAsync();

            // Act
            var response = await webClient.ProductAllGETAsync();

            // Assert
            response.Should().ContainSingle(x => x.Id == product.Id);
        }
    }
}
