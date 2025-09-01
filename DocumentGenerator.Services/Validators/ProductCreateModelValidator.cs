using FluentValidation;
using DocumentGenerator.Services.Contracts.Models;

namespace DocumentGenerator.Services.Validators
{
    /// <summary>
    /// Валидация <see cref="ProductCreateModel"/>
    /// </summary>
    public class ProductCreateModelValidator : AbstractValidator<ProductCreateModel>
    {
        private const int MinLength = 3;
        private const int MaxLength = 255;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductCreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название товара не может быть пустым.")
                .Length(MinLength, MaxLength)
                .WithMessage($"Длина названия товара должна быть от {MinLength} до {MaxLength}");
        }
    }
}
