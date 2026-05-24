namespace DocumentGenerator.Api.Models.Auth;

/// <summary>
/// Модель запроса обновления токена
/// </summary>
public class RefreshTokenRequestApiModel
{
    /// <summary>
    /// Refresh-токен
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
