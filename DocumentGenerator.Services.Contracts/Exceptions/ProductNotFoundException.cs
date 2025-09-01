using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGenerator.Services.Contracts.Exceptions
{
    /// <summary>
    /// Исключение не найденного продукта
    /// </summary>
    public class ProductNotFoundException : ProductException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ProductNotFoundException(string message)
            : base(message) { }
    }
}
