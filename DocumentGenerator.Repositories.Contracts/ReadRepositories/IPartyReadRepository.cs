using DocumentGenerator.Entities;
using System.Linq.Expressions;

namespace DocumentGenerator.Repositories.Contracts.ReadRepositories;

/// <summary>
/// Репозиторий чтения сущности <see cref="Party"/>
/// </summary>
public interface IPartyReadRepository
{
    /// <summary>
    /// Возвращает true, если хотя бы один <see cref="Party"/> в БД удовлетворяет условию, иначе false
    /// </summary>
    Task<bool> Any(Expression<Func<Party, bool>> action, CancellationToken cancellationToken);

    /// <summary>
    /// Получает <see cref="Party"/> по идентификатору
    /// </summary>
    Task<Party?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает коллекцию <see cref="Party"/>
    /// </summary>
    Task<IReadOnlyCollection<Party>> GetAll(CancellationToken cancellationToken);
}
