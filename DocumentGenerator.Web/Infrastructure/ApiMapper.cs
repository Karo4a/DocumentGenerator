using AutoMapper;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Services.Contracts.Models.Product;
using DocumentGenerator.Services.Contracts.Models.Document;
using DocumentGenerator.Web.Models.Party;
using DocumentGenerator.Web.Models.Product;
using DocumentGenerator.Web.Models.Document;

namespace DocumentGenerator.Web.Infrastructure
{
    /// <summary>
    /// Профиль автомаппинга API
    /// </summary>
    public class ApiMapper : Profile
    {
        /// <summary>
        /// Констуктор
        /// </summary>
        public ApiMapper()
        {
            CreateMap<ProductModel, ProductApiModel>(MemberList.Destination).ReverseMap();
            CreateMap<ProductRequestApiModel, ProductCreateModel>(MemberList.Destination);
            CreateMap<ProductRequestApiModel, ProductModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<PartyModel, PartyApiModel>(MemberList.Destination).ReverseMap();
            CreateMap<PartyRequestApiModel, PartyCreateModel>(MemberList.Destination);
            CreateMap<PartyRequestApiModel, PartyModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<DocumentModel, DocumentApiModel>(MemberList.Destination).ReverseMap();
            CreateMap<DocumentRequestApiModel, DocumentCreateModel>(MemberList.Destination);
            CreateMap<DocumentRequestApiModel, DocumentModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}