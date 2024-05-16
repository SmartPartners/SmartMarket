using Newtonsoft.Json;
using SmartMarket.Desktop.Service.Interfrace.Categories;
using SmartMarket.Desktop.Service.StaticModels;
using SmartMarket.Domin.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Service.Categories
{
    public class CategoriesService : ICategoriesService
    {
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
                        var staffs = JsonConvert.DeserializeObject<List<Category>>(content);
                        return staffs;
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
