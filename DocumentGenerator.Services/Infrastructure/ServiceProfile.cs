using AutoMapper;
using DocumentGenerator.Entities;
using DocumentGenerator.Services.Contracts.Models.Party;
using DocumentGenerator.Services.Contracts.Models.Product;
using DocumentGenerator.Services.Contracts.Models.DocumentProduct;
using DocumentGenerator.Services.Contracts.Models.Document;
using DocumentGenerator.Repositories.Contracts.Models;
using DocumentGenerator.Services.Contracts.Models.User;

namespace DocumentGenerator.Services.Infrastructure;

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
        CreateMap<DocumentProductCreateModel, DocumentProduct>(MemberList.Destination);

        CreateMap<DocumentProductDbModel, DocumentProductModel>(MemberList.Destination);
        CreateMap<DocumentProductDbModel, DocumentProduct>(MemberList.Destination)
            .ForMember(x => x.ProductId, opt => opt.MapFrom(y => y.Product.Id));
        CreateMap<DocumentDbModel, DocumentModel>(MemberList.Destination);
        CreateMap<DocumentDbModel, Document>(MemberList.Destination)
            .ForMember(x => x.SellerId, opt => opt.MapFrom(y => y.Seller.Id))
            .ForMember(x => x.BuyerId, opt => opt.MapFrom(y => y.Buyer.Id));

        CreateMap<User, UserModel>(MemberList.Destination)
            .ForMember(x => x.Role, opt => opt.MapFrom(y => y.UserRole.Role));
        CreateMap<UserDbModel, UserModel>(MemberList.Destination)
            .ForMember(x => x.Role, opt => opt.MapFrom(y => y.UserRole.Role));
        CreateMap<UserDbModel, User>(MemberList.Destination)
            .ForMember(x => x.UserRoleId, opt => opt.MapFrom(y => y.UserRole.Id));
    }
}
