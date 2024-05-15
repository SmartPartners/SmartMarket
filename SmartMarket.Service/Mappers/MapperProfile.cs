using AutoMapper;
using SmartMarket.Domin.Entities.Cards;
using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Domin.Entities.CencelOrders;
using SmartMarket.Domin.Entities.ContrAgents;
using SmartMarket.Domin.Entities.Partners;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Domin.Entities.Users;
using SmartMarket.Service.DTOs.CancelOrders;
using SmartMarket.Service.DTOs.Cards;
using SmartMarket.Service.DTOs.Categories;
using SmartMarket.Service.DTOs.ContrAgents;
using SmartMarket.Service.DTOs.PartnerProducts;
using SmartMarket.Service.DTOs.Partners;
using SmartMarket.Service.DTOs.Products;
using SmartMarket.Service.DTOs.Tolov;
using SmartMarket.Service.DTOs.Users;

namespace SmartMarket.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Users
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForChangePasswordDto>().ReverseMap();

        // Category
        CreateMap<Category, CategoryForResultDto>().ReverseMap();
        CreateMap<Category, CategoryForUpdateDto>().ReverseMap();
        CreateMap<Category, CategoryForCreationDto>().ReverseMap();

        // Cards
        CreateMap<Card, CardForResultDto>().ReverseMap();
        CreateMap<Card, CardForUpdateDto>().ReverseMap();
        CreateMap<Card, CardForCreationDto>().ReverseMap();

        // CancelOrder
        CreateMap<CencelOrder, CancelOrderForResultDto>().ReverseMap();
        CreateMap<CencelOrder, CancelOrderForUpdateDto>().ReverseMap();
        CreateMap<CencelOrder, CancelOrderForCreationDto>().ReverseMap();

        //Products
        CreateMap<Product, ProductForResultDto>().ReverseMap();
        CreateMap<Product, ProductForUpdateDto>().ReverseMap();
        CreateMap<Product, ProductForCreationDto>().ReverseMap();

        // Partner
        CreateMap<Partner, PartnerForCreationDto>().ReverseMap();
        CreateMap<Partner, PartnerForResultDto>().ReverseMap();
        CreateMap<Partner, PartnerForUpdateDto>().ReverseMap();

        // PartnerProduct
        CreateMap<PartnerProduct, PartnerProductForCreationDto>().ReverseMap();
        CreateMap<PartnerProduct, PartnerProductForResultDto>().ReverseMap();
        CreateMap<PartnerProduct, PartnerProductForUpdateDto>().ReverseMap();

        // ContrAgent
        CreateMap<ContrAgent, ContrAgentForResultDto>().ReverseMap();
        CreateMap<ContrAgent, ContrAgentForUpdateDto>().ReverseMap();
        CreateMap<ContrAgent, ContrAgentForCreationDto>().ReverseMap();

        // Tolov
        CreateMap<Tolov, TolovForResultDto>().ReverseMap();
    }
}
