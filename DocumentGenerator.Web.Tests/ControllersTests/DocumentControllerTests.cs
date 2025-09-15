using DocumentGenerator.Context;
using DocumentGenerator.Entities;
using DocumentGenerator.Web.Tests.Client;
using DocumentGenerator.Web.Tests.Infrastructure;
using FluentAssertions;
using Xunit;

namespace DocumentGenerator.Web.Tests.ControllersTests
{
    /// <summary>
    /// Интеграционные тесты контроллера документов
    /// </summary>
    [Collection(nameof(DocumentGeneratorCollection))]
    public class DocumentControllerTests
    {
        private readonly IDocumentGeneratorApiClient webClient;
        private readonly DocumentGeneratorContext context;
        private readonly EntitiesGenerator entitiesGenerator;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentControllerTests(DocumentGeneratorApiFixture fixture)
        {
            webClient = fixture.WebClient;
            context = fixture.Context;
            entitiesGenerator = new(context);
        }

        /// <summary>
        /// Возвращает документ по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var entity = await entitiesGenerator.Document();

            // Act
            var response = await webClient.DocumentGETAsync(entity.Id);

            // Assert
            response.Should()
                .BeEquivalentTo(entity, opt => opt
                .Using<object>(ctx =>
                    ctx.Expectation.Should().Be(
                        DateOnly.FromDateTime(((DateTimeOffset)ctx.Subject).Date)))
                .When(expectation => expectation.CompileTimeType == typeof(DateOnly))
                .ExcludingMissingMembers());
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
                await entitiesGenerator.Document();
            await entitiesGenerator.Document(DateTimeOffset.Now);

            // Act
            var response = await webClient.DocumentAllAsync();

            // Assert
            response.Should().NotBeEmpty()
                .And.HaveCount(3);
        }

        /// <summary>
        /// Создает документ
        /// </summary>
        [Fact]
        public async Task CreateShouldReturnValue()
        {
            // Arrange
            var request = await entitiesGenerator.DocumentRequestApiModel();

            // Act
            var response = await webClient.DocumentPOSTAsync(request);

            // Assert
            response.Should().BeEquivalentTo(request, opt => opt
                .ExcludingMissingMembers());
        }

        /// <summary>
        /// Редактирует документ
        /// </summary>
        [Fact]
        public async Task EditShouldReturnValue()
        {
            // Arrange
            var entity = await entitiesGenerator.Document();

            var request = await entitiesGenerator.DocumentRequestApiModel();

            // Act
            var response = await webClient.DocumentPUTAsync(entity.Id, request);

            // Assert
            response.Should().BeEquivalentTo(request, opt => opt
                .ExcludingMissingMembers());
        }

        /// <summary>
        /// Удаляет документ
        /// </summary>
        [Fact]
        public async Task DeleteShouldReturnValue()
        {
            // Arrange
            var entity = await entitiesGenerator.Document();

            // Act
            await webClient.DocumentDELETEAsync(entity.Id);

            // Assert
            var newValue = context.Set<Document>().Single(x => x.Id == entity.Id && x.DeletedAt != null);
            newValue.Should().NotBeNull();
        }
    }
}

