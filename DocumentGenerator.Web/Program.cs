using DocumentGenerator.Api.Client;
using DocumentGenerator.Web.Components;
using DocumentGenerator.Web.Infrastructure;
using DocumentGenerator.Web.Services;

namespace DocumentGenerator.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddBlazorBootstrap();

        var apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl")
            ?? throw new InvalidOperationException("ApiSettings:BaseUrl is not configured");

        builder.Services.AddScoped<IClientTokenStore, ClientTokenStore>();
        builder.Services.AddScoped<UserState>();
        builder.Services.AddScoped<UserStateInitializer>();
        builder.Services.AddTransient<AuthDelegatingHandler>(sp =>
            new AuthDelegatingHandler(
                sp.GetRequiredService<IClientTokenStore>(),
                sp.GetRequiredService<IHttpClientFactory>(),
                apiBaseUrl));

        builder.Services.AddHttpClient<IDocumentGeneratorApiClient, WebDocumentGeneratorApiClient>(
            client => new WebDocumentGeneratorApiClient(apiBaseUrl, client))
            .AddHttpMessageHandler<AuthDelegatingHandler>();

        //builder.Services.AddHttpClient("DocumentGeneratorApi", client =>
        //{
        //    client.BaseAddress = new Uri(apiBaseUrl);
        //})
        //    .AddHttpMessageHandler<AuthDelegatingHandler>();

        //builder.Services.AddScoped<IDocumentGeneratorApiClient>(sp =>
        //{
        //    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
        //    var httpClient = clientFactory.CreateClient("DocumentGeneratorApi");
        //    return new WebDocumentGeneratorApiClient(apiBaseUrl, httpClient);
        //});

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
