using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SmartMarket.Desktop.Service.Interfrace.Categories;
using SmartMarket.Desktop.Service.StaticModels;
using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Service.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Service.Categories
{
    public class CategoriesService : ICategoriesService
    {
        public async Task<Category> CreateAsync(CategoryForCreationDto category)
        {
            try
            {
                //string token = IdentitySingelton.Token;
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{StaticModel.BASE_URL}" + $"/api/Categories");
                //request.Headers.Add("Authorization", $"Bearer {token}");
                var json = JsonConvert.SerializeObject(category);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<BaseModel<Category>>(result);
                    return model.Data!;
                }

                return null;


            }
            catch
            {
                return null;
            }

        }

        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                //string token = IdentitySingelton.Token;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri($"{StaticModel.BASE_URL}" + $"/api/Categories/{id}");
                //client.DefaultRequestHeaders.Add("Authoration", $"Bearer {token}");

                var result = await client.DeleteAsync(client.BaseAddress);
                var response = await result.Content.ReadAsStringAsync();
                var staffs = JsonConvert.DeserializeObject<DeleteModel>(response);

                return staffs.data == true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<IList<Category>> GetcategoriesAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Categories");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var staffs = JsonConvert.DeserializeObject<BaseModelList<Category>>(content);
                        return staffs.Data;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Category> UpdateAsync(long id,CategoryForCreationDto category)
        {
            try
            {
                //tring token = IdentitySingelton.Token;
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Put, $"{StaticModel.BASE_URL}" + $"/api/Categories/{id}");
                //request.Headers.Add("Authorization", $"Bearer {token}");
                var json = JsonConvert.SerializeObject(category);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<BaseModel<Category>>(result);
                    return model.Data!;
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
