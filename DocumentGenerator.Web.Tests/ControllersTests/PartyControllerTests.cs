using Ahatornn.TestGenerator;
using DocumentGenerator.Context;
using DocumentGenerator.Entities;
using DocumentGenerator.Web.Tests.Client;
using DocumentGenerator.Web.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace DocumentGenerator.Web.Tests.ControllersTests
{
    /// <summary>
    /// Интеграционные тесты контроллера сторон актов
    /// </summary>
    [Collection(nameof(DocumentGeneratorCollection))]
    public class PartyControllerTests
    {
        private readonly IDocumentGeneratorApiClient webClient;
        private readonly DocumentGeneratorContext context;
        private readonly EntitiesGenerator entitiesGenerator;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PartyControllerTests(DocumentGeneratorApiFixture fixture)
        {
            webClient = fixture.WebClient;
            context = fixture.Context;
            entitiesGenerator = new(context);
        }

        /// <summary>
        /// Возвращает сторону акта по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var entity = await entitiesGenerator.Party();

            // Act
            var response = await webClient.PartyGETAsync(entity.Id);

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
                await entitiesGenerator.Party();
            await entitiesGenerator.Party(DateTimeOffset.Now);

            // Act
            var response = await webClient.PartyAllAsync();

            // Assert
            response.Should().NotBeEmpty()
                .And.HaveCount(3);
        }

        /// <summary>
        /// Создает сторону акта
        /// </summary>
        [Fact]
        public async Task CreateShouldReturnValue()
        {
            // Arrange
            var request = entitiesGenerator.PartyRequestApiModel();

            // Act
            var response = await webClient.PartyPOSTAsync(request);

            // Assert
            response.Should().BeEquivalentTo(request);
        }

        /// <summary>
        /// Редактирует сторону акта
        /// </summary>
        [Fact]
        public async Task EditShouldReturnValue()
        {
            // Arrange
            var entity = await entitiesGenerator.Party();

            var request = entitiesGenerator.PartyRequestApiModel();

            // Act
            var response = await webClient.PartyPUTAsync(entity.Id, request);

            // Assert
            response.Should().BeEquivalentTo(request);
        }

        /// <summary>
        /// Удаляет сторону акта
        /// </summary>
        [Fact]
        public async Task DeleteShouldReturnValue()
        {
            // Arrange
            var entity = await entitiesGenerator.Party();

            // Act
            await webClient.PartyDELETEAsync(entity.Id);

            // Assert
            var newValue = context.Set<Party>().Single(x => x.Id == entity.Id && x.DeletedAt != null);
            newValue.Should().NotBeNull();
        }
    }
}

