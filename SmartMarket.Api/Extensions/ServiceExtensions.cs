using SmartMarket.Data.IRepositories;
using SmartMarket.Data.Repositories;
using SmartMarket.Service.Interfaces.Categories;
using SmartMarket.Service.Interfaces.Partners;
using SmartMarket.Service.Interfaces.Users;
using SmartMarket.Service.Services.Categories;
using SmartMarket.Service.Services.Partners;
using SmartMarket.Service.Services.Users;

namespace SmartMarket.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPartnerService, PartnerService>();
        services.AddScoped<ICategoryService, CategoryService>();

        // Repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
