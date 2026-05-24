using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts.WriteRepositories;

/// <summary>
/// Репозиторий записи сущности <see cref="RefreshToken"/>
/// </summary>
public interface IRefreshTokenWriteRepository : IDbWriter<RefreshToken>
{
}
