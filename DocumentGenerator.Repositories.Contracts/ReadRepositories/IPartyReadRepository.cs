using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts.ReadRepositories
{
    /// <summary>
    /// Интерфейс чтения сущности <see cref="Party"/>
    /// </summary>
    public interface IPartyReadRepository
    {
        /// <summary>
        /// Получает <see cref="Party"/> по идентификатору
        /// </summary>
        Task<Party?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает коллекцию <see cref="Party"/>
        /// </summary>
        Task<IReadOnlyCollection<Party>> GetAll(CancellationToken cancellationToken);
    }
}
