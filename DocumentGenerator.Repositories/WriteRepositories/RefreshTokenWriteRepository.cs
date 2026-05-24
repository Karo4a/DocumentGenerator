using DocumentGenerator.Common.Contracts;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.WriteRepositories;

namespace DocumentGenerator.Repositories.WriteRepositories;

/// <inheritdoc cref="IRefreshTokenWriteRepository" />
public class RefreshTokenWriteRepository : BaseWriteRepository<RefreshToken>,
    IRefreshTokenWriteRepository,
    IRepositoryAnchor
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public RefreshTokenWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        : base(writer, dateTimeProvider)
    {

    }
}
