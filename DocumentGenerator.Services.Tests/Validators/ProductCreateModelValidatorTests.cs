using DocumentGenerator.Services.Contracts.Models;
using DocumentGenerator.Services.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace DocumentGenerator.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="ProductCreateModelValidator"/>
    /// </summary>
    public class ProductCreateModelValidatorTests
    {
        private readonly ProductCreateModelValidator validator;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductCreateModelValidatorTests()
        {
            validator = new ProductCreateModelValidator();
        }

        /// <summary>
        /// Валидация падает с ошибкой пустого имени
        /// </summary>
        [Fact]
        public async Task ShouldHaveEmptyNameErrorMessage()
        {
            // Arrange
            var model = new ProductCreateModel();

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Валидация падает с ошибкой слишком короткого имени
        /// </summary>
        [Fact]
        public async Task ShouldHaveShortNameErrorMessage()
        {
            // Arrange
            var model = new ProductCreateModel
            { 
                Name = "12",
                Description = string.Empty,
            };

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Валидация падает с ошибкой слишком длинного имени
        /// </summary>
        [Fact]
        public async Task ShouldHaveLongNameErrorMessage()
        {
            // Arrange
            var model = new ProductCreateModel
            {
                Name = new string('1', 300),
                Description = string.Empty,
            };

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Валидация проходит без ошибок
        /// </summary>
        [Fact]
        public async Task ShouldNotHaveErrorMessage()
        {
            // Arrange
            var model = new ProductCreateModel
            {
                Name = "1234",
                Description = string.Empty,
            };

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
        }
    }
}
