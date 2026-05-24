namespace DocumentGenerator.Services.Contracts.Exceptions;

/// <summary>
/// Базовый класс исключений
/// </summary>
public class DocumentGeneratorException : Exception
{
    /// <summary>
    /// Конструктор без параметров
    /// </summary>
    public DocumentGeneratorException() { }

    /// <summary>
    /// Конструктор c указанием сообщения об ошибке
    /// </summary>
    public DocumentGeneratorException(string message)
        : base(message) { }
}
