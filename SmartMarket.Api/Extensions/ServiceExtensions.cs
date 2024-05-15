using SmartMarket.Data.IRepositories;
using SmartMarket.Data.Repositories;
using SmartMarket.Service.Interfaces.Cards;
using SmartMarket.Service.Interfaces.Categories;
using SmartMarket.Service.Interfaces.CencelOrders;
using SmartMarket.Service.Interfaces.ContrAgents;
using SmartMarket.Service.Interfaces.PartnerProducts;
using SmartMarket.Service.Interfaces.Partners;
using SmartMarket.Service.Interfaces.Products;
using SmartMarket.Service.Interfaces.Users;
using SmartMarket.Service.Services.Cards;
using SmartMarket.Service.Services.Categories;
using SmartMarket.Service.Services.CencelOrders;
using SmartMarket.Service.Services.ContrAgents;
using SmartMarket.Service.Services.PartnerProducts;
using SmartMarket.Service.Services.Partners;
using SmartMarket.Service.Services.Products;
using SmartMarket.Service.Services.Users;

namespace SmartMarket.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IPartnerService, PartnerService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICancelOrderService, CancelOrderService>();
        services.AddScoped<IPartnerProductService, PartnerProductService>();
        services.AddScoped<ITolovService, TolovService>();
        services.AddScoped<IContrAgentService, ContrAgentService>();
        // Repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });
    }
}
