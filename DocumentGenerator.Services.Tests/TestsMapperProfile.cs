using AutoMapper;
using DocumentGenerator.Entities;
using DocumentGenerator.Services.Contracts.Models.Document;
using DocumentGenerator.Services.Contracts.Models.DocumentProduct;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Services.Contracts.Models.Product;

namespace DocumentGenerator.Services.Tests
{
    /// <summary>
    /// Профиль автомаппинга тестов
    /// </summary>
    public class TestsMapperProfile : Profile
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public TestsMapperProfile()
        {
            CreateMap<Product, ProductCreateModel>(MemberList.Destination);
            CreateMap<Party, PartyCreateModel>(MemberList.Destination);
            CreateMap<DocumentProduct, DocumentProductCreateModel>(MemberList.Destination);
            CreateMap<Document, DocumentCreateModel>(MemberList.Destination);
        }
    }
}
