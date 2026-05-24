using DocumentGenerator.Api.Models.Enums;

namespace DocumentGenerator.Api.Models.User;

/// <summary>
/// Модель запроса изменения роли пользователя
/// </summary>
public class UserRoleRequestApiModel
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public UserRoleApi Role { get; set; }
}