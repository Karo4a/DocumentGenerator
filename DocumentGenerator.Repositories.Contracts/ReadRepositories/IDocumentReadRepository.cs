using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.Models;
using System.Linq.Expressions;

namespace DocumentGenerator.Repositories.Contracts.ReadRepositories
{
    /// <summary>
    /// Репозиторий чтения сущности <see cref="Document"/>
    /// </summary>
    public interface IDocumentReadRepository
    {
        /// <summary>
        /// Возвращает true, если хотя бы один <see cref="Document"/> в БД удовлетворяет условию, иначе false
        /// </summary>
        Task<bool> Any(Expression<Func<Document, bool>> action, CancellationToken cancellationToken);

        /// <summary>
        /// Получает <see cref="Document"/> по идентификатору
        /// </summary>
        Task<DocumentDbModel?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию <see cref="Document"/>
        /// </summary>
        Task<IReadOnlyCollection<DocumentDbModel>> GetAll(CancellationToken cancellationToken);
    }
}
