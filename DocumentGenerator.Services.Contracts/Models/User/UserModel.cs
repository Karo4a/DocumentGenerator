using DocumentGenerator.Services.Contracts.Models.Enums;

namespace DocumentGenerator.Services.Contracts.Models.User;

/// <summary>
/// Модель пользователя
/// </summary>
public class UserModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Роль пользователя
    /// </summary>
    public UserRole Role { get; set; }
}
