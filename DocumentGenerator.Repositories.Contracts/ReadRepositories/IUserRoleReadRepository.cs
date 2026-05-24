using DocumentGenerator.Entities;
using DocumentGenerator.Entities.Enums;

namespace DocumentGenerator.Repositories.Contracts.ReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="UserRole"/>
/// </summary>
public interface IUserRoleReadRepository
{
    /// <summary>
    /// Получает <see cref="UserRole"/> по идентификатору
    /// </summary>
    Task<UserRole?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="UserRole"/>
    /// </summary>
    Task<IReadOnlyCollection<UserRole>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="UserRole"/> по значению роли
    /// </summary>
    Task<UserRole?> GetByRole(Role role, CancellationToken cancellationToken);
}
