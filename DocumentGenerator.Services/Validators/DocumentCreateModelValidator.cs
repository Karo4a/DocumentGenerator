using FluentValidation;
using DocumentGenerator.Services.Contracts.Models.Document;

namespace DocumentGenerator.Services.Validators
{
    /// <summary>
    /// Валидация <see cref="DocumentCreateModel"/>
    /// </summary>
    public class DocumentCreateModelValidator : AbstractValidator<DocumentCreateModel>
    {
        private const int MinLength = 1;
        private const int MaxLength = 255;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentCreateModelValidator()
        {
            RuleFor(x => x.DocumentNumber)
                .NotEmpty().WithMessage("Номер документа не может быть пустым.")
                .Length(MinLength, MaxLength);

            RuleFor(x => x.ContractNumber)
                .NotEmpty().WithMessage("Номер договора не может быть пустым.")
                .Length(MinLength, MaxLength);

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
                x.RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithMessage("Количество товара не может быть меньше одного.");
                x.RuleFor(x => x.Cost)
                    .GreaterThan(0).WithMessage("Цена товара должна быть больше нуля.");
            });
        }
    }
}
