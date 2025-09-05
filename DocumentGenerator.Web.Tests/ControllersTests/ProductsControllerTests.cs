using Ahatornn.TestGenerator;
using DocumentGenerator.Context;
using DocumentGenerator.Web.Models.Product;
using DocumentGenerator.Web.Tests.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace DocumentGenerator.Web.Tests.ControllersTests
{
    /// <summary>
    /// Интеграционные тесты контроллера товаров
    /// </summary>
    [Collection(nameof(ProductsCollection))]
    public class ProductsControllerTests
    {
        private readonly HttpClient webClient;
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
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Products/");
            var product = TestEntityProvider.Shared.Create<Entities.Product>();
            await context.AddAsync(product);
            await context.SaveChangesAsync();
            
            // Act
            var response = await webClient.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ProductApiModel[]>(json);
            items.Should().ContainSingle(x => x.Id == product.Id);
        }
    }
}
