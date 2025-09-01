using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DocumentGenerator.Context
{
    /// <summary>
    /// Фабрика для создания контекста в DesignTime
    /// </summary>
    public class ProductsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProductsContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context
        /// </summary>
        /// <remarks>
        /// 1) dotnet tool install --global dotnet-ef
        /// 2) dotnet tool update --global dotnet-ef
        /// 3) dotnet ef migrations add [name] --project DocumentGenerator.Context\DocumentGenerator.Context.csproj
        /// 4) dotnet ef database update --project DocumentGenerator.Context\DocumentGenerator.Context.csproj
        /// 5) dotnet ef database update [targetMigrationName] --project DocumentGenerator.Context\DocumentGenerator.Context.csproj
        /// </remarks>
        public ProductsContext CreateDbContext(string[] args)
        {
            var connectionString = "Host=localhost;Port=5432;Database=documentGenerator;Username=postgres;Password=asdasddd";
            var options = new DbContextOptionsBuilder<ProductsContext>()
                .UseNpgsql(connectionString)
                .LogTo(Console.WriteLine)
                .Options;

            return new ProductsContext(options);
        }
    }
}
