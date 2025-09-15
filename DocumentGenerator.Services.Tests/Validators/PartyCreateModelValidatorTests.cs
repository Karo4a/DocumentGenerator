using Ahatornn.TestGenerator;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Services.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace DocumentGenerator.Services.Tests.Validators
{
    /// <summary>
    /// Тесты для <see cref="PartyCreateModelValidator"/>
    /// </summary>
    public class PartyCreateModelValidatorTests
    {
        private readonly PartyCreateModelValidator validator;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PartyCreateModelValidatorTests()
        {
            validator = new PartyCreateModelValidator();
        }

        /// <summary>
        /// Валидация падает с ошибкой пустого имени
        /// </summary>
        [Fact]
        public async Task ShouldHaveEmptyNameErrorMessage()
        {
            // Arrange
            var model = new PartyCreateModel();

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
            var model = TestEntityProvider.Shared.Create<PartyCreateModel>(x =>
            {
                x.Name = "1";
            });

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
            var model = TestEntityProvider.Shared.Create<PartyCreateModel>(x =>
            {
                x.Name = new string('1', 300);
            });

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        /// Валидация падает с ошибкой пустой работы
        /// </summary>
        [Fact]
        public async Task ShouldHaveEmptyJobErrorMessage()
        {
            // Arrange
            var model = new PartyCreateModel();

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Job);
        }

        /// <summary>
        /// Валидация падает с ошибкой пустого ИНН
        /// </summary>
        [Fact]
        public async Task ShouldHaveEmptyTaxIdMessage()
        {
            // Arrange
            var model = new PartyCreateModel();

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TaxId);
        }

        /// <summary>
        /// Валидация падает с ошибкой неправильной длины ИНН
        /// </summary>
        [Fact]
        public async Task ShouldHaveWrongLengthTaxIdErrorMessage()
        {
            // Arrange
            var model = TestEntityProvider.Shared.Create<PartyCreateModel>(x =>
            {
                x.TaxId = new string('0', 11);
            });

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TaxId);
        }

        /// <summary>
        /// Валидация проходит без ошибок
        /// </summary>
        [Fact]
        public async Task ShouldNotHaveErrorMessage()
        {
            // Arrange
            var model = TestEntityProvider.Shared.Create<PartyCreateModel>(x =>
            {
                x.TaxId = new string('0', 10);
            });

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
