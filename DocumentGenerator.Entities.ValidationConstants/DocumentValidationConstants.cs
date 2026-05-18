namespace DocumentGenerator.Entities.ValidationConstants;

/// <summary>
/// Класс констант валидации <see cref="Document"/>
/// </summary>
public class DocumentValidationConstants
{
    /// <summary>
    /// Максимальная длина <see cref="Document.DocumentNumber"/>
    /// </summary>
    public const int DocumentNumberMaxLength = 255;

    /// <summary>
    /// Минимальная длина <see cref="Document.DocumentNumber"/>
    /// </summary>
    public const int DocumentNumberMinLength = 1;

    /// <summary>
    /// Максимальная длина <see cref="Document.ContractNumber"/>
    /// </summary>
    public const int ContractNumberMaxLength = 255;

    /// <summary>
    /// Минимальная длина <see cref="Document.DocumentNumber"/>
    /// </summary>
    public const int ContractNumberMinLength = 1;
}
