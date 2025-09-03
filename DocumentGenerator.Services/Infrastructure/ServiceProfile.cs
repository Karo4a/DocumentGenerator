using AutoMapper;
using DocumentGenerator.Entities;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Services.Contracts.Models.Product;

namespace DocumentGenerator.Services.Infrastructure
{
    /// <summary>
    /// Профиль автомаппинга сервиса
    /// </summary>
    public class ServiceProfile : Profile
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ServiceProfile()
        {
            CreateMap<Product, ProductModel>(MemberList.Destination);
            CreateMap<Party, PartyModel>(MemberList.Destination);
        }
    }
}
