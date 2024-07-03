using Newtonsoft.Json;
using SmartMarket.Desktop.Service.Interfrace.Tolov;
using SmartMarket.Desktop.Service.StaticModels;
using SmartMarket.Service.DTOs.Partners;
using SmartMarket.Service.DTOs.Tolov;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Service.Tolov
{
    public class TolovService : ITolovService
    {
        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Tolov/{id}");

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

        public async Task<IEnumerable<TolovForResultDto>> GetAllAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Tolov");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var staffs = JsonConvert.DeserializeObject<List<TolovForResultDto>>(content);
                        return staffs;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TolovForResultDto> GetByIdAsync(long id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/Tolov/{id}");

                    using (HttpResponseMessage response = await client.GetAsync(client.BaseAddress))
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<TolovForResultDto>(content);
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
