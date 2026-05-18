namespace DocumentGenerator.Entities.ValidationConstants;

/// <summary>
/// Класс констант валидации <see cref="Product"/>
/// </summary>
public class ProductValidationConstants
{
    /// <summary>
    /// Максимальная длина <see cref="Product.Name"/>
    /// </summary>
    public const int NameMaxLength = 255;

    /// <summary>
    /// Минимальная длина <see cref="Product.Name"/>
    /// </summary>
    public const int NameMinLength = 3;
}
