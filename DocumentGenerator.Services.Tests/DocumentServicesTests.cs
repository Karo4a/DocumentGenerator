using AutoMapper;
using DocumentGenerator.Common.Contracts;
using DocumentGenerator.Context.Tests;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.ReadRepositories;
using DocumentGenerator.Repositories.WriteRepositories;
using DocumentGenerator.Services.Contracts.Exceptions;
using DocumentGenerator.Services.Contracts.IServices;
using DocumentGenerator.Services.Contracts.Models.Document;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Services.Contracts.Models.Product;
using DocumentGenerator.Services.Infrastructure;
using DocumentGenerator.Services.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace DocumentGenerator.Services.Tests
{
    /// <summary>
    /// Тесты на <see cref="DocumentServices"/>
    /// </summary>
    public class DocumentServicesTests : DocumentGeneratorContextInMemory
    {
        private readonly IDocumentServices service;
        private readonly IMapper mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentServicesTests()
        {
            mapper = new MapperConfiguration(opts => opts.AddProfile<TestsMapperProfile>()).CreateMapper();

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<ServiceProfile>();
            });
            var serviceMapper = config.CreateMapper();

            service = new DocumentServices(new DocumentReadRepository(Context),
                new DocumentWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
                new DocumentProductWriteRepository(Context, Mock.Of<IDateTimeProvider>()),
                new PartyReadRepository(Context),
                new ProductReadRepository(Context),
                serviceMapper,
                UnitOfWork
                );
        }

        /// <summary>
        /// Падает с ошибкой не найденнго документа
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
        /// Возвращает модель документа
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            // Arrange
            var entity = await PrepareDocument();

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
        /// Возвращает список документов
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            // Arrange
            for (int i = 0; i < 3; ++i)
            {
                await PrepareDocument();
            }
            await PrepareDocument(DateTimeOffset.UtcNow);

            // Act
            var result = await service.GetAll(CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty()
                .And.HaveCount(3);
        }


        /// <summary>
        /// Создание падает с ошибкой существующего документа
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowDuplicateException()
        {
            // Arrange
            var entity = await PrepareDocument();
            var request = mapper.Map<DocumentCreateModel>(entity);
            
            // Act
            var result = () => service.Create(request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorDuplicateException>().WithMessage($"*{entity.DocumentNumber}*");
        }

        /// <summary>
        /// Создание падает с ошибкой не найденного продавца
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowSellerNotFoundException()
        {
            // Arrange
            var entity = await PrepareDocument(save: false);
            var request = mapper.Map<DocumentCreateModel>(entity);
            request.SellerId = Guid.NewGuid();

            // Act
            var result = () => service.Create(request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorNotFoundException>().WithMessage($"*{request.SellerId}*");
        }

        /// <summary>
        /// Создание падает с ошибкой не найденного покупателя
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowBuyerNotFoundException()
        {
            // Arrange
            var entity = await PrepareDocument(save: false);
            var request = mapper.Map<DocumentCreateModel>(entity);
            request.BuyerId = Guid.NewGuid();

            // Act
            var result = () => service.Create(request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorNotFoundException>().WithMessage($"*{request.BuyerId}*");
        }

        /// <summary>
        /// Создание падает с ошибкой не найденного товара
        /// </summary>
        [Fact]
        public async Task CreateShouldThrowProductNotFoundException()
        {
            // Arrange
            var entity = await PrepareDocument(save: false);
            var request = mapper.Map<DocumentCreateModel>(entity);
            request.Products.First().ProductId = Guid.NewGuid();

            // Act
            var result = () => service.Create(request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorNotFoundException>().WithMessage($"*{request.Products.First().ProductId}*");
        }

        /// <summary>
        /// Документ успешно создается
        /// </summary>
        [Fact]
        public async Task CreateShouldWork()
        {
            // Arrange
            var request = mapper.Map<DocumentCreateModel>(await PrepareDocument(save: false));

            // Act
            var result = await service.Create(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(request, opt => opt
                    .ExcludingMissingMembers());
        }

        /// <summary>
        /// Редактирование падает с ошибкой не найденного документа
        /// </summary>
        [Fact]
        public async Task EditShouldThrowNotFoundException()
        {
            // Arrange
            var entity = await PrepareDocument(save: false);
            var request = mapper.Map<DocumentCreateModel>(entity);

            // Act 
            var result = () => service.Edit(entity.Id, request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorNotFoundException>().WithMessage($"*{entity.Id}*");
        }

        /// <summary>
        /// Редактирование падает с ошибкой существующего документа
        /// </summary>
        [Fact]
        public async Task EditShouldThrowDuplicateException()
        {
            // Arrange
            var entity = await PrepareDocument();
            var request = mapper.Map<DocumentCreateModel>(entity);
            var id = Guid.NewGuid();

            // Act
            var result = () => service.Edit(id, request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorDuplicateException>().WithMessage($"*{entity.DocumentNumber}*");
        }

        /// <summary>
        /// Редактирование падает с ошибкой не найденного продавца
        /// </summary>
        [Fact]
        public async Task EditShouldThrowSellerNotFoundException()
        {
            // Arrange
            var entity = await PrepareDocument(save: false);
            var request = mapper.Map<DocumentCreateModel>(entity);
            request.SellerId = Guid.NewGuid();

            // Act
            var result = () => service.Edit(entity.Id, request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorNotFoundException>().WithMessage($"*{request.SellerId}*");
        }

        /// <summary>
        /// Редактирование падает с ошибкой не найденного покупателя
        /// </summary>
        [Fact]
        public async Task EditShouldThrowBuyerNotFoundException()
        {
            // Arrange
            var entity = await PrepareDocument(save: false);
            var request = mapper.Map<DocumentCreateModel>(entity);
            request.BuyerId = Guid.NewGuid();

            // Act
            var result = () => service.Edit(entity.Id, request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorNotFoundException>().WithMessage($"*{request.BuyerId}*");
        }

        /// <summary>
        /// Редактирование падает с ошибкой не найденного товара
        /// </summary>
        [Fact]
        public async Task EditShouldThrowProductNotFoundException()
        {
            // Arrange
            var entity = await PrepareDocument(save: false);
            var request = mapper.Map<DocumentCreateModel>(entity);
            request.Products.First().ProductId = Guid.NewGuid();

            // Act
            var result = () => service.Edit(entity.Id, request, CancellationToken.None);

            // Assert
            await result.Should().ThrowAsync<DocumentGeneratorNotFoundException>().WithMessage($"*{request.Products.First().ProductId}*");
        }

        /// <summary>
        /// Документ успешно редактируется
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var entity = await PrepareDocument();
            var request = mapper.Map<DocumentCreateModel>(entity);

            // Act 
            var result = await service.Edit(entity.Id, request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull()
                .And.BeEquivalentTo(request, opt => opt
                    .ExcludingMissingMembers());
        }

        /// <summary>
        /// Удаление падает с ошибкой не найденного документа
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
        /// Документ успешно удаляется
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var entity = await PrepareDocument();

            // Act
            await service.Delete(entity.Id, CancellationToken.None);

            // Assert
            var newValue = Context.Set<Document>().Single(x => x.Id == entity.Id && x.DeletedAt != null);
            newValue.Should().NotBeNull();
        }
    }
}
