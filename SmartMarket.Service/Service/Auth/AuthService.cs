using Newtonsoft.Json;
using SmartMarket.Service.Commons.Response;
using SmartMarket.Service.DTOs.Auth;
using SmartMarket.Service.Interfrace.Auth;
using System.Text;


namespace SmartMarket.Service.Service.Auth
{
    public class AuthService : IAuthService
    {
        private HttpClient _httpClient;

        public AuthService()
        {
            this._httpClient = Commons.Extensions.CollectionExtensions.GetHttpClient();
        }

        public async Task<BaseResponse<string>> LoginAsync(LoginDTO loginDto)
        {
            try
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{_httpClient.BaseAddress}" + $"Auth/login");
                var json = JsonConvert.SerializeObject(loginDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<BaseResponse<string>>(result);
                    return model!;
                }

                return null;


            }
            catch
            {
                return null;
            }
        }
    }
}
