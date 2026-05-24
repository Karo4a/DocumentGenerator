using System.Text;

namespace DocumentGenerator.Api.Client;

/// <summary>
/// Заглушки для методов PrepareRequestAsync и ProcessResponseAsync,
/// генерируемых NSwag в основном файле клиента
/// </summary>
public partial class DocumentGeneratorApiClient
{
    /// <summary>
    /// Вызывается перед отправкой HTTP-запроса (с финальным URL)
    /// </summary>
    protected virtual Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url, CancellationToken cancellationToken)
        => Task.CompletedTask;

    /// <summary>
    /// Вызывается перед отправкой HTTP-запроса (с построителем URL)
    /// </summary>
    protected virtual Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder, CancellationToken cancellationToken)
        => Task.CompletedTask;

    /// <summary>
    /// Вызывается после получения HTTP-ответа
    /// </summary>
    protected virtual Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
        => Task.CompletedTask;
}
