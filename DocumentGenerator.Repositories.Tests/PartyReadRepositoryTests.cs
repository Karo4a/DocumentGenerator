using DocumentGenerator.Context.Tests;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace DocumentGenerator.ProductRepository.Tests
{
    /// <summary>
    /// Тесты на <see cref="PartyReadRepository"/>
    /// </summary>
    public class PartyReadRepositoryTests : DocumentGeneratorContextInMemory
    {
        private readonly IPartyReadRepository readRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PartyReadRepositoryTests()
        {
            readRepository = new PartyReadRepository(Context);
        }

        /// <summary>
        /// Возвращает значение Null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await readRepository.GetById(id, CancellationToken.None);
            
            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Возвращает значение Null удаленного товара
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNullByDelete()
        {
            // Arrange
            var entity = await PrepareParty(DateTime.UtcNow);

            // Act
            var result = await readRepository.GetById(entity.Id, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Возвращает исходный товар
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var entity = await PrepareParty();

            // Act
            var result = await readRepository.GetById(entity.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(entity);
        }

        /// <summary>
        /// Возвращает пустой список
        /// </summary>
        [Fact]
        public async Task GetAllShouldBeEmpty()
        {
            // Act
            var result = await readRepository.GetAll(CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Возвращает список с товарами
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            // Arrange
            for (int i = 0; i < 3; ++i)
            {
                await PrepareParty();
            }
            await PrepareParty(DateTime.UtcNow);

            // Act
            var result = await readRepository.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .NotBeEmpty()
                .And.BeInAscendingOrder(x => x.Name)
                .And.HaveCount(3);
        }
    }
}
