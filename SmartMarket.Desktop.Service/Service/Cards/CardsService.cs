using Newtonsoft.Json;
using SmartMarket.Desktop.Service.Interfrace.Cards;
using SmartMarket.Desktop.Service.StaticModels;
using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Domin.Entities.Users;
using SmartMarket.Domin.Enums;
using SmartMarket.Service.DTOs.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Service.Cards
{
    public class CardsService : ICardsService
    {
        public async Task<CardForResultDto> CalculeteDiscountPercentageAsync(long id, short discount)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/{id}/{discount}");

                    using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, null))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<CardForResultDto>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> CancelProductAtListByPlanshetAsync(string transNo, string barCode, decimal quantity)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/planshetdan-donalab-yukni-qaytarish?transNo={transNo}&barCode={barCode}&quantity={quantity}");

                    using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, null))
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

        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/{id}");

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
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/transaction-generator");

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

        public async Task<IEnumerable<CardForResultDto>> GetAllAsync(PaginationParams paginationParams)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<IEnumerable<CardForResultDto>>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CardForResultDto>> GetAllHistory(long userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/tarixni-korish/{userId}/{startDate}/{endDate}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<IEnumerable<CardForResultDto>>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CardForResultDto>> GetAllMaxSells(int max)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/kop-sotilgan-yuk/{max}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<IEnumerable<CardForResultDto>>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CardForResultDto> GetByBarCodeAsync(string barcode)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/barcode-orqali-olish/{barcode}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<CardForResultDto>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CardForResultDto> GetByIdAsync(long id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/{id}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<CardForResultDto>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CardForResultDto>> GetBystatusAsync(string status)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/status-orqali-qidiruv/{status}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<IEnumerable<CardForResultDto>>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CardForResultDto> SaleProductWithBarCodeAsync(string barcode, long userId, decimal quantity, string transNo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/barcode-bilan-sotish/{barcode}/{userId}/{quantity}/{transNo}");

                    using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, null))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<CardForResultDto>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CardForResultDto> SaleProductWithId(long id, long userId, decimal quantity, string transNo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Cards/id-bilan-sotish/{id}/{userId}/{quantity}/{transNo}");

                    using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, null))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<CardForResultDto>(content);
                        return result;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<CardForResultDto> UpdateWithTransactionNumberAsync(string transNo, TolovUsuli tolovUsuli)
        {
            throw new NotImplementedException();
        }
    }
}
