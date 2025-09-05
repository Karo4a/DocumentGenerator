using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts.ReadRepositories
{
    /// <summary>
    /// Репозиторий чтения сущности <see cref="Document"/>
    /// </summary>
    public interface IDocumentReadRepository
    {
        /// <summary>
        /// Получает <see cref="Document"/> по идентификатору
        /// </summary>
        Task<Document?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию <see cref="Document"/>
        /// </summary>
        Task<IReadOnlyCollection<Document>> GetAll(CancellationToken cancellationToken);
    }
}
