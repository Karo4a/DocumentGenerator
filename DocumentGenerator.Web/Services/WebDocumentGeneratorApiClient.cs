namespace DocumentGenerator.Web.Services;

internal class WebDocumentGeneratorApiClient(string baseUrl, HttpClient httpClient)
    : DocumentGenerator.Api.Client.DocumentGeneratorApiClient(baseUrl, httpClient);
