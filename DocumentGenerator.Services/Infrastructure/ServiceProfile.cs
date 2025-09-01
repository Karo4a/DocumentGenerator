
using AutoMapper;
using DocumentGenerator.Entities;
using DocumentGenerator.Services.Contracts.Models;

namespace DocumentGenerator.Services.Infrastructure
{
    /// <summary>
    /// Профиль автомаппинга сервиса
    /// </summary>
    public class ServiceProfile : Profile
    {
        /// <summary>
        /// Констуктор
        /// </summary>
        public ServiceProfile()
        {
            CreateMap<Product, ProductModel>(MemberList.Destination);
        }
    }
}
