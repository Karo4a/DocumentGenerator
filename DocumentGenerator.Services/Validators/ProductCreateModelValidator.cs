using FluentValidation;
using DocumentGenerator.Services.Contracts.Models.Product;
using DocumentGenerator.Entities.ValidationConstants;

namespace DocumentGenerator.Services.Validators;

/// <summary>
/// Валидация <see cref="ProductCreateModel"/>
/// </summary>
public class ProductCreateModelValidator : AbstractValidator<ProductCreateModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public ProductCreateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название товара не может быть пустым.")
            .Length(ProductValidationConstants.NameMinLength, ProductValidationConstants.NameMaxLength)
            .WithMessage($"Длина названия товара должна быть от {ProductValidationConstants.NameMinLength} до {ProductValidationConstants.NameMaxLength}");

        RuleFor(x => x.Cost)
            .GreaterThan(0).WithMessage("Цена товара должна быть больше нуля.");
    }
}
