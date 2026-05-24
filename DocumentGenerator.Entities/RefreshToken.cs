using DocumentGenerator.Entities.Contracts;

namespace DocumentGenerator.Entities;

/// <summary>
/// Сущность токена обновления
/// </summary>
public class RefreshToken : IEntityWithId, IEntityWithSoftDeleted
{
    /// <inheritdoc cref="IEntityWithId.Id"/>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата истечения срока действия токена
    /// </summary>
    public DateTimeOffset Expires { get; set; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Отметка безопасности пользователя на момент создания токена
    /// </summary>
    public string SecurityStamp { get; set; } = string.Empty;

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <inheritdoc cref="IEntityWithSoftDeleted.DeletedAt"/>
    public DateTimeOffset? DeletedAt { get; set; }
}
