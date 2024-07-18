using Newtonsoft.Json;
using SmartMarket.Desktop.Service.Interfrace.Users;
using SmartMarket.Desktop.Service.StaticModels;
using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Entities.Users;
using SmartMarket.Service.DTOs.Users;
using SmartMarket.Service.Helpers;
using System.Text;

namespace SmartMarket.Desktop.Service.Service.Users;

public class UserService : IUserService
{
    public async Task<User> CreateAsync(UserForCreationDto creationDto)
    {
        try
        {
            //string token = IdentitySingelton.Token;
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{StaticModel.BASE_URL}" + $"/api/Users");
            request.Headers.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(creationDto.FirstName), "FirstName");
            content.Add(new StringContent(creationDto.LastName), "LastName");
            content.Add(new StringContent(creationDto.PhoneNumber), "PhoneNumber");
            content.Add(new StringContent(creationDto.Role.ToString()), "Role");
            content.Add(new StringContent(creationDto.IsActive.ToString()), "IsActive");
            content.Add(new StringContent(creationDto.Oylik.ToString()), "Address");
            content.Add(new StringContent(creationDto.Password), "Password");

            request.Content = content;

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<User>(result);
                return model!;
            }

            return new User();

        }
        catch
        {
            return new User();
        }

    }

    public async Task<IList<User>> GetAllAsync()
    {
        try
        {
            PaginationParams paginationParams = new PaginationParams();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{StaticModel.BASE_URL}" + $"/api/Users?PageSize={paginationParams.PageSize}&PageIndex={paginationParams.PageIndex}");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");
            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
            string response = await message.Content.ReadAsStringAsync();
            List<User> posts = JsonConvert.DeserializeObject<List<User>>(response)!;

            return posts;
        }
        catch
        {
            return new List<User> { };
        }

    }

    public Task<User> LoginAsync()
    {
        throw new NotImplementedException();
    }
}
