namespace DocumentGenerator.Api.Models.User;

/// <summary>
/// Модель запроса создания/обновления пользователя
/// </summary>
public class UserRequestApiModel
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
}
