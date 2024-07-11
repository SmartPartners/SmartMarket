using Newtonsoft.Json;
using SmartMarket.Desktop.Service.Interfrace.ContrAgents;
using SmartMarket.Desktop.Service.StaticModels;
using SmartMarket.Domin.Entities.ContrAgents;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Service.DTOs.ContrAgents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Service.ContrAgents
{
    public class ContrAgentService : IContrAgentService
    {
        public async Task<ContrAgent> CreateAsync(ContrAgentForCreationDto dto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(StaticModel.BASE_URL + $"/api/ContrAgents");

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(dto.Firma), "Firma");
                content.Add(new StringContent(dto.FirstName), "FirstName");
                content.Add(new StringContent(dto.LastName), "LastName");
                content.Add(new StringContent(dto.PhoneNumber), "PhoneNumber");


                using (HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var contrAgent = JsonConvert.DeserializeObject<BaseModel<ContrAgent>>(result);
                        return contrAgent.Data;
                    }
                    return null;
                }
            }
        }

        public async Task<IList<ContrAgent>> GetContrAgentsAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri($"{StaticModel.BASE_URL}" + $"/api/ContrAgents");
                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
                string response = await message.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BaseModelList<ContrAgent>>(response);
                return result.Data!;
            }
            catch
            {
                return null;
            }
        }
    }
}
