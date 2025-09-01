using DocumentGenerator.Common;
using System.Diagnostics.CodeAnalysis;

namespace DocumentGenerator.Context.Contracts
{
    /// <summary>
    /// Базовый класс репозитория записи данных
    /// </summary>
    public abstract class BaseWriteRepository<T> : IDbWriter<T> where T : class
    {
        private readonly IWriter writer;
        private readonly IDateTimeProvider dateTimeProvider;

        /// <summary>
        /// Конструктор
        /// </summary>
        public BaseWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
        {
            this.writer = writer;
            this.dateTimeProvider = dateTimeProvider;
        }

        void IDbWriter<T>.Add([NotNull] T entity)
        {
            AuditCreate(entity);
            AuditUpdate(entity);
            writer.Add(entity);
        }

        void IDbWriter<T>.Edit([NotNull] T entity)
        {
            AuditUpdate(entity);
            writer.Edit(entity);
        }

        void IDbWriter<T>.Delete([NotNull] T entity)
        {
            if (entity is IEntityWithSoftDeleted softEntity)
            {
                AuditUpdate(entity);
                softEntity.DeletedAt = dateTimeProvider.UtcNow();
                writer.Edit(softEntity);
            } else
            {
                writer.Delete(entity);
            }
        }

        private void AuditCreate([NotNull] T entity)
        {
            if (entity is IEntityWithAudit auditCreated)
            {
                auditCreated.CreatedAt = dateTimeProvider.UtcNow();
            }
        }

        private void AuditUpdate([NotNull] T entity)
        {
            if (entity is IEntityWithAudit auditCreated)
            {
                auditCreated.UpdatedAt = dateTimeProvider.UtcNow();
            }
        }
    }
}
