namespace DocumentGenerator.Services.Contracts.Models.Auth;

/// <summary>
/// Модель обновления токена
/// </summary>
public class RefreshTokenCreateModel
{
    /// <summary>
    /// Refresh-токен
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
