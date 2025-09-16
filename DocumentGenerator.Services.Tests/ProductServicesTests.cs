using AutoMapper;
using DocumentGenerator.Common.Contracts;
using DocumentGenerator.Context.Tests;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.ReadRepositories;
using DocumentGenerator.Repositories.WriteRepositories;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Product;
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
        private readonly IMapper mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductServicesTests()
        {
            mapper = new MapperConfiguration(opts => opts.AddProfile<TestsMapperProfile>()).CreateMapper();

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<ServiceProfile>();
            });
            var serviceMapper = config.CreateMapper();

            service = new ProductServices(new ProductReadRepository(Context),
                new ProductWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
                serviceMapper,
                UnitOfWork
                );
        }

        /// <summary>
        /// Падает с ошибкой не найденного товара
        /// </summary>
        [Fact]
        public async Task GetByIdShouldThrowNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act 
            var result = () => service.GetById(id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorNotFoundException>().WithMessage($"*{id}*");

        }

        /// <summary>
        /// Возвращает модель товара
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var entity = await PrepareProduct();

            // Act 
            var result = await service.GetById(entity.Id, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(entity, opt => opt
                .ExcludingMissingMembers());
        }

        /// <summary>
        /// Возвращает пустой список
        /// </summary>
        [Fact]
        public async Task GetAllShouldBeEmpty()
        {
            // Act
            var result = await service.GetAll(CancellationToken.None);

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
                await PrepareProduct();
            }
            await PrepareProduct(DateTimeOffset.UtcNow);

            // Act
            var result = await service.GetAll(CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty()
                .And.HaveCount(3);
        }

        /// <summary>
        /// Создание падает с ошибкой существующего товара
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowDuplicateException()
        {
            // Arrange
            var entity = await PrepareProduct();
            var request = mapper.Map<ProductCreateModel>(entity);
            
            // Act
            var result = () => service.Create(request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorDuplicateException>().WithMessage($"*{entity.Name}*");
        }

        /// <summary>
        /// Товар успешно создается
        /// </summary>
        [Fact]
        public async Task CreateShouldWork()
        {
            // Arrange
            var request = mapper.Map<ProductCreateModel>(await PrepareProduct(save: false));

            // Act
            var result = await service.Create(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(request);
        }

        /// <summary>
        /// Редактирование падает с ошибкой не найденного товара
        /// </summary>
        [Fact]
        public async Task EditShouldThrowNotFoundException()
        {
            // Arrange
            var entity = await PrepareProduct(save: false);
            var request = mapper.Map<ProductCreateModel>(entity);

            // Act 
            var result = () => service.Edit(entity.Id, request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorNotFoundException>().WithMessage($"*{entity.Id}*");
        }

        /// <summary>
        /// Редактирование падает с ошибкой существующего товара
        /// </summary>
        [Fact]
        public async Task EditShouldThrowDuplicateException()
        {
            // Arrange
            var entity = await PrepareProduct();
            var request = mapper.Map<ProductCreateModel>(entity);
            var id = Guid.NewGuid();

            // Act
            var result = () => service.Edit(id, request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorDuplicateException>().WithMessage($"*{entity.Name}*");
        }

        /// <summary>
        /// Товар успешно редактируется
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var entity = await PrepareProduct();
            var request = mapper.Map<ProductCreateModel>(entity);

            // Act 
            var result = await service.Edit(entity.Id, request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(request);
        }

        /// <summary>
        /// Удаление падает с ошибкой не найденного товара
        /// </summary>
        [Fact]
        public async Task DeleteShouldThrowNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = () => service.Delete(id, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorNotFoundException>().WithMessage($"*{id}*");
        }

        /// <summary>
        /// Товар успешно удаляется
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var entity = await PrepareProduct();

            // Act
            await service.Delete(entity.Id, CancellationToken.None);

            // Assert
            var newValue = Context.Set<Product>().Single(x => x.Id == entity.Id && x.DeletedAt != null);
            newValue.Should().NotBeNull();
        }
    }
}
