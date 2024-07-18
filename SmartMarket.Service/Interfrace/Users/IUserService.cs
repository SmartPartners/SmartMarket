using SmartMarket.Domin.Entities.Users;
using SmartMarket.Service.DTOs.Users;

namespace SmartMarket.Desktop.Service.Interfrace.Users;

public interface IUserService
{
    public Task<User> LoginAsync();

    public Task<User> CreateAsync(UserForCreationDto creationDto);

    public Task<IList<User>> GetAllAsync();
}
