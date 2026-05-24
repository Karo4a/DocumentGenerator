using AutoMapper;
using DocumentGenerator.Api.Infrastructure;
using DocumentGenerator.Common;
using DocumentGenerator.Common.Contracts;
using DocumentGenerator.Common.Mvc.Extensions;
using DocumentGenerator.Common.Mvc.Models;
using DocumentGenerator.Context;
using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Repositories;
using DocumentGenerator.Services;
using DocumentGenerator.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace DocumentGenerator.Api;

/// <summary>
/// Класс программы
/// </summary>
public class Program
{
    /// <summary>
    /// Точка входа программы
    /// </summary>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // https://support.aspnetzero.com/QA/Questions/11011/Cannot-write-DateTime-with-KindLocal-to-PostgreSQL-type-%27timestamp-with-time-zone%27-only-UTC-is-supported
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        var controllers = builder.Services.AddControllers(opt =>
        {
            opt.Filters.Add<DocumentGeneratorExceptionFilter>();
        });

        if (builder.Environment.EnvironmentName == EnvironmentProvider.IntegrationEnviroment)
        {
            controllers.AddControllersAsServices();
        }

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Введите ваш JWT токен"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            var baseDirectory = AppContext.BaseDirectory;
            c.IncludeXmlComments(Path.Combine(baseDirectory, "DocumentGenerator.Api.xml"));
            c.IncludeXmlComments(Path.Combine(baseDirectory, "DocumentGenerator.Entities.xml"));
            c.SchemaFilter<EnumNameSchemaFilter>();
            c.EnableAnnotations();
        });

        builder.Services.AddAuth(builder.Configuration);
        builder.Services.AddAuthorization();
        builder.Services.AddDataProtection();

        var jwtSettings = builder.Configuration.GetSection(JwtSettingsModel.Key).Get<JwtSettingsModel>()!;
        builder.Services.AddSingleton(jwtSettings);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<DocumentGeneratorContext>(options =>
            options.UseSqlServer(connectionString)
                .LogTo(Console.WriteLine)
        );
        builder.Services.AddScoped<IReader>(x => x.GetRequiredService<DocumentGeneratorContext>());
        builder.Services.AddScoped<IWriter>(x => x.GetRequiredService<DocumentGeneratorContext>());
        builder.Services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<DocumentGeneratorContext>());

        builder.Services.RegisterAssemblyInterfacesAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);

        builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        builder.Services.AddSingleton<IVatRateProvider, VatRateProvider>();

        builder.Services.AddSingleton(_ =>
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ServiceProfile>();
                cfg.AddProfile<ApiMapper>();
            });

            var mapper = mapConfig.CreateMapper();
            return mapper;
        });

        builder.Services.RegisterAssemblyInterfacesAssignableTo<IRepositoryAnchor>(ServiceLifetime.Scoped);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorClient", policy =>
            {
                policy.WithOrigins("http://localhost:5234", "https://localhost:7259")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("AllowBlazorClient");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
