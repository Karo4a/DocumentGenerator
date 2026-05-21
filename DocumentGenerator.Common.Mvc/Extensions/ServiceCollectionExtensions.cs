using DocumentGenerator.Common.Mvc.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace DocumentGenerator.Common.Mvc.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет аутентификацию
    /// </summary>
    public static void AddAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var authSetting = configuration.GetSection(JwtSettingsModel.Key).Get<JwtSettingsModel>()!;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = authSetting.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = authSetting.Issuer,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(authSetting.ClockSkewSec),
                    ValidateIssuerSigningKey = true,
                    TokenDecryptionKey = new SymmetricSecurityKey(
                        Base64UrlEncoder.DecodeBytes(authSetting.SecretKeyBase64)),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Base64UrlEncoder.DecodeBytes(authSetting.SignKeyBase64))
                };
            });
    }

    /// <summary>
    /// Регистрирует все интерфейсы инстансов в указанной сборке для указанного маркерного интерфейса
    /// </summary>
    /// <param name="services"><inheritdoc cref="IServiceCollection"/></param>
    /// <param name="lifetime"><inheritdoc cref="ServiceLifetime"/></param>
    /// <typeparam name="TInterface">Тип, для которого осуществляется регистрация</typeparam>
    public static void RegisterAssemblyInterfacesAssignableTo<TInterface>(this IServiceCollection services, ServiceLifetime lifetime)
    {
        var serviceType = typeof(TInterface);
        var types = serviceType.Assembly.GetTypes()
            .Where(p => serviceType.IsAssignableFrom(p) &&
                        !(p.IsAbstract ||
                          p.IsInterface));
        foreach (var type in types)
        {
            services.TryAdd(new ServiceDescriptor(type, type, lifetime));
            var interfaces = type.GetTypeInfo()
                .ImplementedInterfaces
                .Where(i => i != typeof(IDisposable) &&
                            i.IsPublic &&
                            i != serviceType);

            foreach (Type interfaceType in interfaces)
            {
                services.TryAdd(new ServiceDescriptor(interfaceType,
                    provider => provider.GetRequiredService(type),
                    lifetime));
            }
        }
    }
}
