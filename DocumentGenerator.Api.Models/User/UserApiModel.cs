using DocumentGenerator.Api.Models.Enums;

namespace DocumentGenerator.Api.Models.User;

/// <summary>
/// Модель пользователя
/// </summary>
public class UserApiModel
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
    public UserRoleApi Role { get; set; }
}
