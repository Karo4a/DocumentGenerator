using FluentValidation;
using DocumentGenerator.Services.Contracts.Models.Document;
using DocumentGenerator.Entities.ValidationConstants;

namespace DocumentGenerator.Services.Validators;

/// <summary>
/// Валидация <see cref="DocumentCreateModel"/>
/// </summary>
public class DocumentCreateModelValidator : AbstractValidator<DocumentCreateModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public DocumentCreateModelValidator()
    {
        RuleFor(x => x.DocumentNumber)
            .NotEmpty().WithMessage("Номер документа не может быть пустым.")
            .Length(DocumentValidationConstants.DocumentNumberMinLength, DocumentValidationConstants.DocumentNumberMaxLength)
            .WithMessage($"Длина номера документа должна быть от {DocumentValidationConstants.DocumentNumberMinLength} до {DocumentValidationConstants.DocumentNumberMaxLength}");

        RuleFor(x => x.ContractNumber)
            .NotEmpty().WithMessage("Номер договора не может быть пустым.")
            .Length(DocumentValidationConstants.ContractNumberMinLength, DocumentValidationConstants.ContractNumberMaxLength)
            .WithMessage($"Длина номера договора должна быть от {DocumentValidationConstants.ContractNumberMinLength} до {DocumentValidationConstants.ContractNumberMaxLength}");

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Дата подписания документа не может быть в будущем");

        RuleFor(x => x.SellerId)
            .NotEmpty().WithMessage("Идентификатор продавца не может быть пустым.")
            .NotEqual(x => x.BuyerId).WithMessage("Идентификаторы покупателя и продавца не могут совпадать");

        RuleFor(x => x.BuyerId)
            .NotEmpty().WithMessage("Идентификатор продавца не может быть пустым.");

        RuleFor(x => x.Products)
            .Must(x => x.Select(y => y.ProductId).Distinct().Count() == x.Count)
            .WithMessage("Товары не могут повторяться.")
            .NotEmpty().WithMessage("Список товаров не может быть пустым.");

        RuleForEach(x => x.Products).ChildRules(x =>
        {
            x.RuleFor(y => y.Quantity)
                .GreaterThan(0).WithMessage("Количество товара не может быть меньше одного.");
            x.RuleFor(y => y.Cost)
                .GreaterThan(0).WithMessage("Цена товара должна быть больше нуля.");
        });
    }
}
