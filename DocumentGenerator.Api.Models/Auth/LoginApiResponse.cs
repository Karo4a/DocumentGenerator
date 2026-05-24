namespace DocumentGenerator.Api.Models.Auth;

/// <summary>
/// Модель ответа при успешном входе
/// </summary>
public class LoginApiResponse
{
    /// <summary>
    /// Access-токен
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Refresh-токен
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
