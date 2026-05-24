namespace DocumentGenerator.Web.Services;

/// <summary>
/// Состояние текущего авторизованного пользователя
/// </summary>
public class UserState
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; private set; } = string.Empty;

    /// <summary>
    /// Адрес электронной почты
    /// </summary>
    public string Email { get; private set; } = string.Empty;

    /// <summary>
    /// Роль пользователя
    /// </summary>
    public string Role { get; private set; } = string.Empty;

    /// <summary>
    /// Признак аутентификации пользователя
    /// </summary>
    public bool IsAuthenticated => Id != Guid.Empty;

    /// <summary>
    /// Признак наличия роли администратора
    /// </summary>
    public bool IsAdmin => Role == "Admin";

    /// <summary>
    /// Признак наличия роли редактора или администратора
    /// </summary>
    public bool IsEditor => Role is "Editor" or "Admin";

    /// <summary>
    /// Заполняет состояние из полученных данных пользователя
    /// </summary>
    public void SetFrom(Guid id, string login, string email, string role)
    {
        Id = id;
        Login = login;
        Email = email;
        Role = role;
    }

    /// <summary>
    /// Сбрасывает состояние пользователя
    /// </summary>
    public void Clear()
    {
        Id = Guid.Empty;
        Login = string.Empty;
        Email = string.Empty;
        Role = string.Empty;
    }
}
