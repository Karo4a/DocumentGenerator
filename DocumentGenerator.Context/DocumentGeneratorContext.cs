using DocumentGenerator.Context.Contracts;
using DocumentGenerator.Entities.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DocumentGenerator.Context
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class DocumentGeneratorContext : DbContext, IReader, IWriter, IUnitOfWork
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentGeneratorContext(DbContextOptions<DocumentGeneratorContext> options)
            : base(options)
        {
            // https://support.aspnetzero.com/QA/Questions/11011/Cannot-write-DateTime-with-KindLocal-to-PostgreSQL-type-%27timestamp-with-time-zone%27-only-UTC-is-supported
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);
        }

        IQueryable<TEntity> IReader.Read<TEntity>()
            where TEntity : class
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IWriter.Add<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        void IWriter.Edit<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        void IWriter.Delete<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }

            return count;
        }
    }
}
