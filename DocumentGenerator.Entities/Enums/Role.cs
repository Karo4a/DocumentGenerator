namespace DocumentGenerator.Entities.Enums
{
    /// <summary>
    /// Роли пользователя
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// Может просматривать и экспортировать данные
        /// </summary>
        Viewer = 0,

        /// <summary>
        /// Может создавать и редактировать данные
        /// </summary>
        Editor = 1,

        /// <summary>
        /// Может удалять данные, а также редактировать роли пользователей
        /// </summary>
        Admin = 2,
    }
}
