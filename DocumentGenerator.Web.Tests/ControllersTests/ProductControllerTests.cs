using DocumentGenerator.Context;
using DocumentGenerator.Entities;
using DocumentGenerator.Web.Tests.Client;
using DocumentGenerator.Web.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace DocumentGenerator.Web.Tests.ControllersTests
{
    /// <summary>
    /// Интеграционные тесты контроллера товаров
    /// </summary>
    [Collection(nameof(DocumentGeneratorCollection))]
    public class ProductControllerTests
    {
        private readonly IDocumentGeneratorApiClient webClient;
        private readonly DocumentGeneratorContext context;
        private readonly EntitiesGenerator entitiesGenerator;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductControllerTests(DocumentGeneratorApiFixture fixture)
        {
            webClient = fixture.WebClient;
            context = fixture.Context;
            entitiesGenerator = new(context);
        }

        /// <summary>
        /// Возвращает товар по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var entity = await entitiesGenerator.Product();

            // Act
            var response = await webClient.ProductGETAsync(entity.Id);

            // Assert
            response.Should().BeEquivalentTo(entity, opt => opt
                .Excluding(x => x.CreatedAt)
                .Excluding(x => x.UpdatedAt)
                .Excluding(x => x.DeletedAt));
        }

        /// <summary>
        /// Возвращает все значения
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            // Arrange
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            for (int i = 0; i < 3; ++i)
                await entitiesGenerator.Product();
            await entitiesGenerator.Product(DateTimeOffset.Now);

            // Act
            var response = await webClient.ProductAllAsync();

            // Assert
            response.Should().NotBeEmpty()
                .And.HaveCount(3);
        }

        /// <summary>
        /// Создает товар
        /// </summary>
        [Fact]
        public async Task CreateShouldReturnValue()
        {
            // Arrange
            var request = entitiesGenerator.ProductRequestApiModel();

            // Act
            var response = await webClient.ProductPOSTAsync(request);

            // Assert
            response.Should().BeEquivalentTo(request);
        }

        /// <summary>
        /// Редактирует товар
        /// </summary>
        [Fact]
        public async Task EditShouldReturnValue()
        {
            // Arrange
            var entity = await entitiesGenerator.Product();

            var request = entitiesGenerator.ProductRequestApiModel();

            // Act
            var response = await webClient.ProductPUTAsync(entity.Id, request);

            // Assert
            response.Should().BeEquivalentTo(request);
        }

        /// <summary>
        /// Удаляет товар
        /// </summary>
        [Fact]
        public async Task DeleteShouldReturnValue()
        {
            // Arrange
            var entity = await entitiesGenerator.Product();

            // Act
            await webClient.ProductDELETEAsync(entity.Id);

            // Assert
            var newValue = context.Set<Product>().Single(x => x.Id == entity.Id && x.DeletedAt != null);
            newValue.Should().NotBeNull();
        }
    }
}
