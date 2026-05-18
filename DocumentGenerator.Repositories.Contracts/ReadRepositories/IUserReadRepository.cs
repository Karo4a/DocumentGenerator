using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.Models;
using System.Linq.Expressions;

namespace DocumentGenerator.Repositories.Contracts.ReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="User"/>
/// </summary>
public interface IUserReadRepository
{
    /// <summary>
    /// Возвращает true, если хотя бы один <see cref="User"/> в БД удовлетворяет условию, иначе false
    /// </summary>
    Task<bool> Any(Expression<Func<User, bool>> action, CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="User"/> по идентификатору
    /// </summary>
    Task<UserDbModel?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="User"/>
    /// </summary>
    Task<IReadOnlyCollection<UserDbModel>> GetAll(CancellationToken cancellationToken);
}
