using DocumentGenerator.Services.Contracts.Models.Enums;
using DocumentGenerator.Services.Contracts.Models.User;

namespace DocumentGenerator.Services.Contracts.IServices;

/// <summary>
/// Сервис по работе с пользователями
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Возвращает <see cref="UserModel"/> по идентификатору
    /// </summary>
    Task<UserModel> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает список <see cref="UserModel"/>
    /// </summary>
    Task<IReadOnlyCollection<UserModel>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Добавляет новый <see cref="UserModel"/> 
    /// </summary>
    Task<UserModel> Create(UserCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Изменяет роль пользователя
    /// </summary>
    Task<UserModel> ChangeRole(Guid id, UserRole role, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет существующий <see cref="UserModel"/> из базы данных
    /// </summary>
    Task Delete(Guid id, CancellationToken cancellationToken);
}
