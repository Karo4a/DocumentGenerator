namespace DocumentGenerator.Entities.ValidationConstants;

/// <summary>
/// Класс констант валидации <see cref="User"/>
/// </summary>
public class UserValidationConstants
{
    /// <summary>
    /// Максимальная длина <see cref="User.Login"/>
    /// </summary>
    public const int LoginMaxLength = 255;

    /// <summary>
    /// Минимальная длина <see cref="User.Login"/>
    /// </summary>
    public const int LoginMinLength = 3;

    /// <summary>
    /// Максимальная длина <see cref="User.Email"/>
    /// </summary>
    public const int EmailMaxLength = 255;

    /// <summary>
    /// Максимальная длина <see cref="User.PasswordHash"/>
    /// </summary>
    public const int PasswordHashMaxLength = 44;

    /// <summary>
    /// Максимальная длина <see cref="User.PasswordSalt"/>
    /// </summary>
    public const int PasswordSaltMaxLength = 44;
}
