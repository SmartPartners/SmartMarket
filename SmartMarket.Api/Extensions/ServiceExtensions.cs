using SmartMarket.Data.IRepositories;
using SmartMarket.Data.Repositories;
using SmartMarket.Service.Interfaces.Cards;
using SmartMarket.Service.Interfaces.Categories;
using SmartMarket.Service.Interfaces.CencelOrders;
using SmartMarket.Service.Interfaces.ContrAgents;
using SmartMarket.Service.Interfaces.Kassas;
using SmartMarket.Service.Interfaces.Korzinkas;
using SmartMarket.Service.Interfaces.Orders;
using SmartMarket.Service.Interfaces.PartnerProducts;
using SmartMarket.Service.Interfaces.Partners;
using SmartMarket.Service.Interfaces.Products;
using SmartMarket.Service.Interfaces.TolovUsuli;
using SmartMarket.Service.Interfaces.Users;
using SmartMarket.Service.Services.Cards;
using SmartMarket.Service.Services.Categories;
using SmartMarket.Service.Services.CencelOrders;
using SmartMarket.Service.Services.ContrAgents;
using SmartMarket.Service.Services.Kassas;
using SmartMarket.Service.Services.Korzinkas;
using SmartMarket.Service.Services.Orders;
using SmartMarket.Service.Services.PartnerProducts;
using SmartMarket.Service.Services.Partners;
using SmartMarket.Service.Services.Products;
using SmartMarket.Service.Services.TolovUsul;
using SmartMarket.Service.Services.Users;

namespace SmartMarket.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        // Services
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICancelOrderService, CancelOrderService>();
        services.AddScoped<IContrAgentService, ContrAgentService>();
        services.AddScoped<ITolovService, TolovService>();
        services.AddScoped<IKassaService, KassaService>();
        services.AddScoped<IKorzinkaService, KorzinkaService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPartnerProductService, PartnerProductService>();
        services.AddScoped<IPartnerService, PartnerService>();
        services.AddScoped<IPartnerTolovService, PartnerTolovService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ITolovUsuliService, TolovUsuliService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWorkersPaymentService, WorkersPaymentService>();
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
