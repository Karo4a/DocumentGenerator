using Ahatornn.TestGenerator;
using AutoMapper;
using DocumentGenerator.Common.Contracts;
using DocumentGenerator.Context.Tests;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.ReadRepositories;
using DocumentGenerator.Repositories.WriteRepositories;
using DocumentGenerator.Services.Contracts.Models.Product;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Infrastructure;
using DocumentGenerator.Services.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace DocumentGenerator.Services.Tests
{
    /// <summary>
    /// Тесты на <see cref="ProductServices"/>
    /// </summary>
    public class ProductServicesTests : DocumentGeneratorContextInMemory
    {
        private readonly IProductServices service;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductServicesTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<ServiceProfile>();
            });
            var mapper = config.CreateMapper();

            service = new ProductServices(new ProductReadRepository(Context),
                new ProductWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
                mapper,
                UnitOfWork
                );
        }

        /// <summary>
        /// Сервис возвращает список с товарами
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            // Arrange
            var product1 = TestEntityProvider.Shared.Create<Product>();
            var product2 = TestEntityProvider.Shared.Create<Product>();
            var product3 = TestEntityProvider.Shared.Create<Product>();
            var product4 = TestEntityProvider.Shared.Create<Product>(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.AddRangeAsync(product1, product2, product3, product4);
            await UnitOfWork.SaveChangesAsync();

            // Act
            var result = await service.GetAll(CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty()
                .And.HaveCount(3);
        }

        /// <summary>
        /// Товар создается
        /// </summary>
        [Fact]
        public async Task CreateShouldCreate()
        {
            // Arrange
            var request = TestEntityProvider.Shared.Create<ProductCreateModel>();

            // Act
            var result = await service.Create(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(request);
        }
    }
}
