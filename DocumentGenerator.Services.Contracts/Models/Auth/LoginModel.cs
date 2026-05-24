namespace DocumentGenerator.Services.Contracts.Models.Auth;

/// <summary>
/// Модель входа
/// </summary>
public class LoginModel
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
