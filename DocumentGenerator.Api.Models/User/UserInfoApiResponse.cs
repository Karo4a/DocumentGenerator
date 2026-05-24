using DocumentGenerator.Api.Models.Enums;

namespace DocumentGenerator.Api.Models.User;

/// <summary>
/// Информация о текущем авторизованном пользователе
/// </summary>
public class UserInfoApiResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Адрес электронной почты
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Роль пользователя
    /// </summary>
    public UserRoleApi Role { get; set; }
}
