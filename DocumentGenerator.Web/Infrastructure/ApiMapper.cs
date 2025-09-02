using AutoMapper;
using DocumentGenerator.Services.Contracts.Models;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Web.Models.Party;
using DocumentGenerator.Web.Models.Product;

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
        }
    }
}