using DocumentGenerator.Services.Contracts.Models.Auth;

namespace DocumentGenerator.Services.Contracts.IServices;

/// <summary>
/// Сервис по работе с аутентификацией
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Выполняет аутентификацию и возвращает токены
    /// </summary>
    Task<LoginResponseModel> Login(LoginModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет пару токенов по refresh-токену
    /// </summary>
    Task<LoginResponseModel> UpdateRefreshToken(RefreshTokenCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Отзывает refresh-токен пользователя
    /// </summary>
    Task Logout(Guid userId, CancellationToken cancellationToken);
}
