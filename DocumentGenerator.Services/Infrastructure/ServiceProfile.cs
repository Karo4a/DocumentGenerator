using AutoMapper;
using DocumentGenerator.Entities;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Services.Contracts.Models.Product;
using DocumentGenerator.Services.Contracts.Models.DocumentProduct;
using DocumentGenerator.Services.Contracts.Models.Document;

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
            CreateMap<DocumentProduct, DocumentProductModel>(MemberList.Destination);
            CreateMap<DocumentProductCreateModel, DocumentProduct>(MemberList.Destination);
            CreateMap<Document, DocumentModel>(MemberList.Destination);
        }
    }
}
