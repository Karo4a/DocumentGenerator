using Ahatornn.TestGenerator;
using DocumentGenerator.Services.Contracts.Models.Document;
using DocumentGenerator.Services.Contracts.Models.DocumentProduct;
using DocumentGenerator.Services.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace DocumentGenerator.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="PartyCreateModeDocumentCreateModelValidatorlValidator"/>
    /// </summary>
    public class DocumentCreateModelValidatorTests
    {
        private readonly DocumentCreateModelValidator validator;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentCreateModelValidatorTests()
        {
            validator = new DocumentCreateModelValidator();
        }

        /// <summary>
        /// Валидация падает с ошибкой пустого номера документа
        /// </summary>
        [Fact]
        public async Task ShouldHaveEmptyDocumentNumberErrorMessage()
        {
            // Arrange
            var model = new DocumentCreateModel();

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DocumentNumber);
        }

        /// <summary>
        /// Валидация падает с ошибкой пустого номера контракта
        /// </summary>
        [Fact]
        public async Task ShouldHaveEmptyContractNumberErrorMessage()
        {
            // Arrange
            var model = new DocumentCreateModel();

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DocumentNumber);
        }

        /// <summary>
        /// Валидация падает с ошибкой даты в будущем
        /// </summary>
        [Fact]
        public async Task ShouldHaveDateInFutureErrorMessage()
        {
            // Arrange
            var model = new DocumentCreateModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1))
            };

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Date);
        }

        /// <summary>
        /// Валидация падает с ошибкой пустого идентификатора продавца
        /// </summary>
        [Fact]
        public async Task ShouldHaveEmptySellerIdErrorMessage()
        {
            // Arrange
            var model = new DocumentCreateModel();

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SellerId);
        }

        /// <summary>
        /// Валидация падает с ошибкой пустого идентификатора покупателя
        /// </summary>
        [Fact]
        public async Task ShouldHaveEmptyBuyerIdErrorMessage()
        {
            // Arrange
            var model = new DocumentCreateModel();

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SellerId);
        }

        /// <summary>
        /// Валидация падает с ошибкой одинаковых идентификаторов сторон актов
        /// </summary>
        [Fact]
        public async Task ShouldHaveSamePartyIdsErrorMessage()
        {
            // Arrange
            var model = TestEntityProvider.Shared.Create<DocumentCreateModel>(x =>
            {
                x.BuyerId = x.SellerId;
            });

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SellerId);
        }

        /// <summary>
        /// Валидация падает с ошибкой пустого списка товаров
        /// </summary>
        [Fact]
        public async Task ShouldHaveEmptyProductsErrorMessage()
        {
            // Arrange
            var model = new DocumentCreateModel();

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Products);
        }

        /// <summary>
        /// Валидация падает с ошибкой одинаковых идентификаторов продуктов
        /// </summary>
        [Fact]
        public async Task ShouldHaveSameProductsErrorMessage()
        {
            // Arrange
            var model = TestEntityProvider.Shared.Create<DocumentCreateModel>(x =>
            {
                x.Products.Add(TestEntityProvider.Shared.Create<DocumentProductCreateModel>());
                x.Products.Add(x.Products.First());
            });

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Products);
        }


        /// <summary>
        /// Валидация проходит без ошибок
        /// </summary>
        [Fact]
        public async Task ShouldNotHaveErrorMessage()
        {
            // Arrange
            var model = TestEntityProvider.Shared.Create<DocumentCreateModel>(x =>
            {
                x.Date = DateOnly.FromDateTime(DateTime.Now);
                x.Products.Add(TestEntityProvider.Shared.Create<DocumentProductCreateModel>(x =>
                {
                    x.Quantity = 1;
                    x.Cost = 1;
                }));
            });

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
