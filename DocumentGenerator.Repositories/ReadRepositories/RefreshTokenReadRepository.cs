using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using Microsoft.EntityFrameworkCore;

namespace DocumentGenerator.Repositories.ReadRepositories;

/// <inheritdoc cref="IRefreshTokenReadRepository" />
public class RefreshTokenReadRepository : IRefreshTokenReadRepository, IRepositoryAnchor
{
    private readonly IReader reader;

    /// <summary>
    /// Конструктор
    /// </summary>
    public RefreshTokenReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<RefreshToken?> IRefreshTokenReadRepository.GetById(Guid id, CancellationToken cancellationToken)
        => reader.Read<RefreshToken>()
            .NotDeletedAt()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    Task<RefreshToken?> IRefreshTokenReadRepository.GetByUserId(Guid userId, CancellationToken cancellationToken)
        => reader.Read<RefreshToken>()
            .NotDeletedAt()
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
}
