using FluentValidation;
using DocumentGenerator.Services.Contracts.Models.Party;

namespace DocumentGenerator.Services.Validators
{
    /// <summary>
    /// Валидация <see cref="PartyCreateModel"/>
    /// </summary>
    public class PartyCreateModelValidator : AbstractValidator<PartyCreateModel>
    {
        private const int MinLength = 3;
        private const int MaxLength = 255;
        private const int MinTaxIdLength = 10;
        private const int MaxTaxIdLength = 12;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PartyCreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Полное имя стороны акта не может быть пустым.")
                .Length(MinLength, MaxLength)
                .WithMessage($"Длина полного имени стороны акта должна быть от {MinLength} до {MaxLength}");

            RuleFor(x => x.Job)
                .NotEmpty().WithMessage("Должность стороны акта не может быть пустой.");

            RuleFor(x => x.TaxId)
                .NotEmpty().WithMessage("ИНН стороны акта не может быть пустым.")
                .Length(MinLength, MaxLength)
                .WithMessage($"Длина ИНН стороны акта должна быть от {MinTaxIdLength} до {MaxTaxIdLength}");
        }
    }
}
