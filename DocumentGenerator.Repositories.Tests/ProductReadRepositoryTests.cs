using Ahatornn.TestGenerator;
using DocumentGenerator.Context.Tests;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using DocumentGenerator.Repositories.ReadRepositories;
using FluentAssertions;
using Xunit;

namespace DocumentGenerator.ProductRepository.Tests
{
    /// <summary>
    /// Тесты на <see cref="ProductReadRepository"/>
    /// </summary>
    public class ProductReadRepositoryTests : DocumentGeneratorContextInMemory
    {
        private readonly IProductReadRepository productReadRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductReadRepositoryTests()
        {
            productReadRepository = new ProductReadRepository(Context);
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
            var result = await productReadRepository.GetById(id, CancellationToken.None);
            
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
            var product = TestEntityProvider.Shared.Create<Product>(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.AddAsync(product);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await productReadRepository.GetById(product.Id, CancellationToken.None);

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
            var product = TestEntityProvider.Shared.Create<Product>();
            await Context.AddAsync(product);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await productReadRepository.GetById(product.Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(product);
        }

        /// <summary>
        /// Возвращает пустой список
        /// </summary>
        [Fact]
        public async Task GetAllShouldBeEmpty()
        {
            // Act
            var result = await productReadRepository.GetAll(CancellationToken.None);

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
            var product1 = TestEntityProvider.Shared.Create<Product>(x => x.Name = "2");
            var product2 = TestEntityProvider.Shared.Create<Product>(x => x.Name = "1");
            var product3 = TestEntityProvider.Shared.Create<Product>(x => x.Name = "3");
            var product4 = TestEntityProvider.Shared.Create<Product>(x =>
            {
                x.Name = "3";
                x.DeletedAt = DateTimeOffset.UtcNow;
            });
            await Context.AddRangeAsync(product1, product2, product3, product4);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await productReadRepository.GetAll(CancellationToken.None);

            // Assert
            result.Should()
                .NotBeEmpty()
                .And.BeInAscendingOrder(x => x.Name)
                .And.HaveCount(3);
        }
    }
}
