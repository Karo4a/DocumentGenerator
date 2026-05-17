using System.Text;

namespace DocumentGenerator.Web.Tests.Client
{
    /// <summary>
    /// Имитация клиента для Api запросов
    /// </summary>
    public partial class DocumentGeneratorApiClient
    {
        Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder, CancellationToken cancellationToken)
            => Task.CompletedTask;

        Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string? url, CancellationToken cancellationToken)
            => Task.CompletedTask;

        Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}