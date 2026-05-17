using DocumentGenerator.Context;
using DocumentGenerator.Context.Contracts;
using EfSchemaCompare;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Xunit.Priority;

namespace DocumentGenerator.Database.Tests
{
    /// <summary>
    /// Класс для тестов структуры базы данных
    /// </summary>
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [DefaultPriority(1)]
    public class DatabaseTests
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=documentGenerator;Integrated Security=True";
        private IDatabaseModelFactory databaseModelFactory;
        private DatabaseModel actualDbModel;

        /// <summary>
        /// Контекст <see cref="DocumentGeneratorContext"/>
        /// </summary>
        protected DocumentGeneratorContext Context { get; }

        /// <inheritdoc cref="IUnitOfWork" />
        protected IUnitOfWork UnitOfWork => Context;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DatabaseTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DocumentGeneratorContext>()
                .UseSqlServer(connectionString)
                .Options;
            Context = new DocumentGeneratorContext(optionsBuilder);

            databaseModelFactory = Context.GetDatabaseModelFactory();
            actualDbModel = databaseModelFactory.Create(
                Context.Database.GetDbConnection(),
                new DatabaseModelFactoryOptions());
        }

        /// <summary>
        /// Тестирует возможность подключения к базе данных
        /// </summary>
        [Fact, Priority(0)]
        public async Task TestConnectionCapability()
        {
            // Act
            var isConnected = await Context.Database.CanConnectAsync();

            // Assert
            isConnected.Should().BeTrue();
        }

        /// <summary>
        /// Проверяет существует ли в базе данных таблицы
        /// </summary>
        [Theory, Priority(1)]
        [InlineData("Products")]
        [InlineData("Parties")]
        [InlineData("DocumentProducts")]
        [InlineData("Documents")]
        [InlineData("Users")]
        public void TableShouldExists(string tableName)
        {
            // Act
            var isExists = actualDbModel.Tables.Select(x => x.Name).Contains(tableName);

            // Assert
            isExists.Should().BeTrue();
        }

        /// <summary>
        /// Проверяет соответствуют ли столбцы таблицы столбцам модели Entity Framework
        /// </summary>
        [Theory, Priority(2)]
        [InlineData("Products")]
        [InlineData("Parties")]
        [InlineData("DocumentProducts")]
        [InlineData("Documents")]
        [InlineData("Users")]
        public void TableColumnsShouldMatchEfModelColumns(string tableName)
        {
            // Arrange
            var efModel = Context.Model.GetRelationalModel();

            // Act
            var efTableColumns = efModel.Tables.Where(x => x.Name == tableName).First().Columns.Select(x => x.Name);
            var actualTableColumns = actualDbModel.Tables.Where(x => x.Name == tableName).First().Columns.Select(x => x.Name);
            var missingColumns = efTableColumns.Except(actualTableColumns);
            var excessColumns = actualTableColumns.Except(efTableColumns);

            // Assert
            missingColumns.Should().BeEmpty();
            excessColumns.Should().BeEmpty();
        }

        /// <summary>
        /// Проверяет соответствует ли база данных модели Entity Framework
        /// </summary>
        [Fact, Priority(3)]
        public void DatabaseShouldMatchEfModel()
        {
            // Arrange
            var comparer = new CompareEfSql();

            // Act
            comparer.CompareEfWithDb(Context);
            var errors = comparer.GetAllErrors;

            // Assert
            errors.Should().BeEmpty();
        }
    }
}
