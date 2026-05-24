using DocumentGenerator.Entities;
using DocumentGenerator.Repositories.Contracts.Models;

namespace DocumentGenerator.Services.Contracts.IServices;

/// <summary>
/// Сервис генерации JWT-токенов
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Создаёт access-токен (JWE) для указанного пользователя
    /// </summary>
    string CreateAccessToken(UserDbModel user);

    /// <summary>
    /// Создаёт сущность refresh-токена для указанного пользователя
    /// </summary>
    RefreshToken CreateRefreshToken(UserDbModel user);
}
