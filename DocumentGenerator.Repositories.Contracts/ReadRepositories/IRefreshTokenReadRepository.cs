using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts.ReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="RefreshToken"/>
/// </summary>
public interface IRefreshTokenReadRepository
{
    /// <summary>
    /// Получает <see cref="RefreshToken"/> по идентификатору
    /// </summary>
    Task<RefreshToken?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="RefreshToken"/> по идентификатору пользователя
    /// </summary>
    Task<RefreshToken?> GetByUserId(Guid userId, CancellationToken cancellationToken);
}
