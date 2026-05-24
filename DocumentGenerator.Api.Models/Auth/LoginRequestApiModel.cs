namespace DocumentGenerator.Api.Models.Auth;

/// <summary>
/// Модель запроса входа
/// </summary>
public class LoginRequestApiModel
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
