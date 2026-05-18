using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.ReadRepositories;
using Microsoft.EntityFrameworkCore;

namespace DocumentGenerator.Repositories.ReadRepositories;

/// <inheritdoc cref="IUserRoleReadRepository" />
public class UserRoleReadRepository : IUserRoleReadRepository, IRepositoryAnchor
{
    private readonly IReader reader;

    /// <summary>
    /// Конструктор
    /// </summary>
    public UserRoleReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<UserRole?> IUserRoleReadRepository.GetById(Guid id, CancellationToken cancellationToken)
        => reader.Read<UserRole>()
            .NotDeletedAt()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);

    Task<IReadOnlyCollection<UserRole>> IUserRoleReadRepository.GetAll(CancellationToken cancellationToken)
        => reader.Read<UserRole>()
            .NotDeletedAt()
            .OrderBy(x => x.Role)
            .ToReadOnlyCollectionAsync(cancellationToken);
}
