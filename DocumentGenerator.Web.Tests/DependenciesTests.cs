using DocumentGenerator.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Xunit;

namespace DocumentGenerator.Web.Tests
{
    /// <summary>
    /// Тесты зависимостей
    /// </summary>
    public class DependenciesTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DependenciesTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestAppConfiguration();
                builder.UseEnvironment("integration");
            });
        }

        /// <summary>
        /// Проверка резолва зависимостей
        /// </summary>
        [Theory]
        [MemberData(nameof(WebControllerCore))]
        public void ControllerCoreShouldBeResolved(Type controller)
        {
            // Arrange
            using var scope = factory.Services.CreateScope();

            // Act
            var instance = scope.ServiceProvider.GetRequiredService(controller);

            // Assert
            instance.Should().NotBeNull();
        }

        /// <summary>
        /// Контроллер товаров
        /// </summary>
        public static TheoryData<Type> WebControllerCore => GetControllers<ProductsController>();

        private static TheoryData<Type> GetControllers<TControllers>() =>
            new(Assembly.GetAssembly(typeof(TControllers))?
                .DefinedTypes
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract));
    }
}
