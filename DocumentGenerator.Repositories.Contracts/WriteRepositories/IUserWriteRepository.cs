using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities;

namespace DocumentGenerator.Repositories.Contracts.WriteRepositories;

/// <summary>
/// Репозиторий записи сущности <see cref="User"/>
/// </summary>
public interface IUserWriteRepository : IDbWriter<User>
{
}
