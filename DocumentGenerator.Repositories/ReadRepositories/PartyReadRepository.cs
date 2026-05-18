using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DocumentGenerator.Repositories.ReadRepositories;

/// <inheritdoc cref="IPartyReadRepository" />
public class PartyReadRepository : IPartyReadRepository, IRepositoryAnchor
{
    private readonly IReader reader;

    /// <summary>
    /// Конструктор
    /// </summary>
    public PartyReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<bool> IPartyReadRepository.Any(Expression<Func<Party, bool>> action, CancellationToken cancellationToken)
        => reader.Read<Party>()
            .NotDeletedAt()
            .AnyAsync(action, cancellationToken);

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
