using Newtonsoft.Json;
using SmartMarket.Desktop.Service.Interfrace.CancelOrders;
using SmartMarket.Desktop.Service.StaticModels;
using SmartMarket.Domin.Entities.ContrAgents;
using SmartMarket.Domin.Entities.Users;
using SmartMarket.Service.DTOs.CancelOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Service.CancelOrders
{
    public class CancelOrderService : ICancelOrderService
    {
        public async Task<CancelOrderForResultDto> CanceledProductsAsync(long id, decimal quantity, long canceledBy, string reason, bool action)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/CancelOrders/magazinda-sotilgan-mahsulotlar-uchun?id={id}&quantity={quantity}&canceledBy={canceledBy}&reason={reason}&action={action}");

                string json = "";
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var resultDTO = JsonConvert.DeserializeObject<CancelOrderForResultDto>(result);
                        return resultDTO;
                    }
                    return null;
                }
            }
        }

        public async Task<CancelOrderForResultDto> CanceledProductsFromPArterAsync(long id, long partnerId, decimal quantity, long canceledBy, string reason, bool action)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/CancelOrders/hamkorlardan-qaytgan-mahsulotlar-uchun?id={id}&quantity={quantity}&canceledBy={canceledBy}&reason={reason}&action={action}");

                string json = "";
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var resultDTO = JsonConvert.DeserializeObject<CancelOrderForResultDto>(result);
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
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/CancelOrders/{id}");

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

        public async Task<IEnumerable<CancelOrderForResultDto>> GetAllAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/CancelOrders");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<CancelOrderForResultDto>>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CancelOrderForResultDto>> getAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/CancelOrders/ikkita-vaqt-orasida-magazindagi-mahsulotlarni-kurish/{userId}/{startDate}/{endDate}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<CancelOrderForResultDto>>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CancelOrderForResultDto> GetByIdAsync(long id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/CancelOrders/{id}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<CancelOrderForResultDto>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CancelOrderForResultDto>> YaroqsizMahsulotlarAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/CancelOrders/yaroqsiz-mahsulotlarni-korish/{startDate}/{endDate}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<CancelOrderForResultDto>>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
