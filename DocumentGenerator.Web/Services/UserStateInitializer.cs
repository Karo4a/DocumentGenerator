using DocumentGenerator.Api.Client;

namespace DocumentGenerator.Web.Services;

/// <summary>
/// Инициализатор состояния текущего пользователя
/// </summary>
public class UserStateInitializer
{
    private readonly UserState userState;
    private readonly IDocumentGeneratorApiClient apiClient;

    /// <summary>
    /// Конструктор
    /// </summary>
    public UserStateInitializer(UserState userState, IDocumentGeneratorApiClient apiClient)
    {
        this.userState = userState;
        this.apiClient = apiClient;
    }

    /// <summary>
    /// Запрашивает информацию о текущем пользователе через API и заполняет <see cref="UserState"/>
    /// </summary>
    public async Task InitializeAsync()
    {
        if (userState.IsAuthenticated)
            return;

        try
        {
            var response = await apiClient.UserGetCurrentInfoAsync();

            userState.SetFrom(
                response.Id,
                response.Login,
                response.Email,
                response.Role.ToString());
        }
        catch
        {
            userState.Clear();
        }
    }
}
