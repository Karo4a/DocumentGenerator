namespace DocumentGenerator.Web.Services;

/// <summary>
/// Хранилище JWT-токенов на стороне клиента
/// </summary>
public interface IClientTokenStore
{
    /// <summary>
    /// Возвращает access-токен
    /// </summary>
    Task<string?> GetAccessTokenAsync();

    /// <summary>
    /// Возвращает refresh-токен
    /// </summary>
    Task<string?> GetRefreshTokenAsync();

    /// <summary>
    /// Сохраняет пару токенов
    /// </summary>
    Task SetTokensAsync(string accessToken, string refreshToken);

    /// <summary>
    /// Очищает сохранённые токены
    /// </summary>
    Task ClearAsync();
}
