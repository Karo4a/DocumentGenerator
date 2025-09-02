using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using Microsoft.EntityFrameworkCore;

namespace DocumentGenerator.Repositories.ReadRepositories
{

    public class PartyReadRepository : IPartyReadRepository
    {
        private readonly IReader reader;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PartyReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<Party?> IPartyReadRepository.GetById(Guid id, CancellationToken cancellationToken)
            => reader.Read<Party>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<IReadOnlyCollection<Party>> IPartyReadRepository.GetAll(CancellationToken cancellationToken)
            => reader.Read<Party>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);
    }
}
