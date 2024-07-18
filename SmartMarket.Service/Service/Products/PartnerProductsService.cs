using Newtonsoft.Json;
using SmartMarket.Desktop.Service.Interfrace.Products;
using SmartMarket.Desktop.Service.StaticModels;
using SmartMarket.Domin.Entities.ContrAgents;
using SmartMarket.Domin.Entities.Users;
using SmartMarket.Domin.Enums;
using SmartMarket.Service.DTOs.PartnerProducts;
using SmartMarket.Service.DTOs.Partners;
using SmartMarket.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Service.Products
{
    public class PartnerProductsService : IPartnerProductsService
    {
        public async Task<PartnerProductForResultDto> CalculeteDiscountPercentageAsync(long id, short discountPercentage)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/PartnerProducts/chegirma-berish/{id}/{discountPercentage}");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                string json = "";
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var resultDTO = JsonConvert.DeserializeObject<PartnerProductForResultDto>(result);
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
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/PartnerProducts/{id}");
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

        public async Task<string> GenerateTransactionNumber()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/PartnerProducts/transaction-generator");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<string>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PartnerProductForResultDto>> GetAllAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/PartnerProducts");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<PartnerProductForResultDto>>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PartnerProductForResultDto>> GetAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/PartnerProducts/yuk-tarixini-korish/{userId}/{startDate}/{endDate}");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<PartnerProductForResultDto>>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PartnerProductForResultDto> GetByIdAsync(long id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/PartnerProducts/{id}");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PartnerProductForResultDto>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PartnerProductForResultDto> GetByTransNoAsync(string transNo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/PartnerProducts/get-by-transaction-number?transNo={transNo}");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PartnerProductForResultDto>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PartnerProductForResultDto> ModifyAsync(long id, PartnerProductForUpdateDto dto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/PartnerProducts/{id}");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                string json = JsonConvert.SerializeObject(dto);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var resultDTO = JsonConvert.DeserializeObject<PartnerProductForResultDto>(result);
                        return resultDTO;
                    }
                    return null;
                }
            }
        }

        public async Task<PartnerProductForResultDto> MoveProductToPartnerProductAsync(long id, long partnerId, long userId, decimal quantityToMove, string transNo)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/PartnerProducts/hamkor-uchun-yuk?productId={id}&partnerId={partnerId}&userId{userId}&quantityToMove{quantityToMove}&transNo{transNo}");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                string json = "";
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var resultDTO = JsonConvert.DeserializeObject<PartnerProductForResultDto>(result);
                        return resultDTO;
                    }
                    return null;
                }
            }
        }

        public async Task<PartnerForResultDto> PayForProductsAsync(long partnerId, decimal paid, TolovUsuli tolovUsuli)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/PartnerProducts/pay-for-product?partnerId={partnerId}&paid{partnerId}&tolovUsuli{tolovUsuli}");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");

                string json = "";
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
