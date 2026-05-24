using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts.Models;

/// <summary>
/// Модель пользователя для запроса из базы данных
/// </summary>
public class UserDbModel
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
    /// Хэш пароля
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Соль пароля
    /// </summary>
    public string PasswordSalt { get; set; } = string.Empty;

    /// <summary>
    /// Отметка безопасности
    /// </summary>
    public Guid SecurityStamp { get; set; }

    /// <summary>
    /// Навигационное свойство <see cref="UserRole"/>
    /// </summary>
    public UserRole UserRole { get; set; } = null!;
}
