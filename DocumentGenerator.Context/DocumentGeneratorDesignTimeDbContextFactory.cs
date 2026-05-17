using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DocumentGenerator.Context
{
    /// <summary>
    /// Фабрика для создания контекста в DesignTime
    /// </summary>
    public class DocumentGeneratorDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DocumentGeneratorContext>
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
        public DocumentGeneratorContext CreateDbContext(string[] args)
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=documentGenerator;Integrated Security=True";
            var options = new DbContextOptionsBuilder<DocumentGeneratorContext>()
                .UseSqlServer(connectionString)
                .LogTo(Console.WriteLine)
                .Options;

            return new DocumentGeneratorContext(options);
        }
    }
}
