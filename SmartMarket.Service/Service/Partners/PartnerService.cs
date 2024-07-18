using Newtonsoft.Json;
using SmartMarket.Desktop.Service.Interfrace.Partners;
using SmartMarket.Desktop.Service.StaticModels;
using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Domin.Entities.ContrAgents;
using SmartMarket.Service.DTOs.Partners;
using SmartMarket.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Service.Partners
{
    public class PartnerService : IPartnerService
    {
        public async Task<PartnerForResultDto> CreateAsync(PartnerForCreationDto entity)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Partners");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                string json = JsonConvert.SerializeObject(entity);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var resultDTO = JsonConvert.DeserializeObject<PartnerForResultDto>(result);
                        return resultDTO;
                    }
                    return null;
                }
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Partners/{id}");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                    using (HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<bool>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<PartnerForResultDto>> GetAllAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Partners");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var staffs = JsonConvert.DeserializeObject<List<PartnerForResultDto>>(content);
                        return staffs;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PartnerForResultDto> GetByIdAsync(long id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Partners/{id}");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var staffs = JsonConvert.DeserializeObject<PartnerForResultDto>(content);
                        return staffs;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PartnerForResultDto> ModifyAsync(long id, PartnerForUpdateDto dto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Partners/{id}");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                string json = JsonConvert.SerializeObject(dto);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PutAsync(client.BaseAddress, content))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var resultDTO = JsonConvert.DeserializeObject<PartnerForResultDto>(result);
                        return resultDTO;
                    }
                    return null;
                }
            }
        }
    }
}
