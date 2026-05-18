using FluentValidation;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Entities.ValidationConstants;

namespace DocumentGenerator.Services.Validators;

/// <summary>
/// Валидация <see cref="PartyCreateModel"/>
/// </summary>
public class PartyCreateModelValidator : AbstractValidator<PartyCreateModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public PartyCreateModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Полное имя стороны акта не может быть пустым.")
            .Length(PartyValidationConstants.NameMinLength, PartyValidationConstants.NameMaxLength)
            .WithMessage($"Длина полного имени стороны акта должна быть от {PartyValidationConstants.NameMinLength} до {PartyValidationConstants.NameMaxLength}");

        RuleFor(x => x.Job)
            .NotEmpty().WithMessage("Должность стороны акта не может быть пустой.");

        RuleFor(x => x.TaxId)
            .NotEmpty().WithMessage("ИНН стороны акта не может быть пустым.")
            .Must(x => x.Length == PartyValidationConstants.IndividualTaxIdLength || x.Length == PartyValidationConstants.LegalTaxIdLength)
            .WithMessage($"Длина ИНН стороны акта должна быть либо {PartyValidationConstants.IndividualTaxIdLength} для физических лиц, либо {PartyValidationConstants.LegalTaxIdLength} для юридических лиц");
    }
}
