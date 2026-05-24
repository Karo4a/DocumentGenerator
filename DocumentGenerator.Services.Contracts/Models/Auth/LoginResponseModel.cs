namespace DocumentGenerator.Services.Contracts.Models.Auth;

/// <summary>
/// Модель ответа при успешном входе
/// </summary>
public class LoginResponseModel
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
