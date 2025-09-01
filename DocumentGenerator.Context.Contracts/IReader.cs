using Microsoft.EntityFrameworkCore;

namespace DocumentGenerator.Context.Contracts
{
    /// <summary>
    /// Интерфейс получения записей из контекста
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Предоставляет функциональные возможности
        /// </summary>
        IQueryable<TEntity> Read<TEntity>()
            where TEntity : class;
    }
}
