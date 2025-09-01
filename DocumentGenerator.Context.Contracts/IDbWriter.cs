using System.Diagnostics.CodeAnalysis;

namespace DocumentGenerator.Context.Contracts
{
    public interface IDbWriter<in TEntity> where TEntity : class
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add([NotNull] TEntity entity);

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Edit([NotNull] TEntity entity);

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete([NotNull] TEntity entity);
    }
}
