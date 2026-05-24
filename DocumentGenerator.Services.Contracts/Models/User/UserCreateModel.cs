using DocumentGenerator.Services.Contracts.Models.Enums;

namespace DocumentGenerator.Services.Contracts.Models.User;

/// <summary>
/// Модель создания пользователя
/// </summary>
public class UserCreateModel
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Роль пользователя
    /// </summary>
    public UserRole Role { get; set; }
}
