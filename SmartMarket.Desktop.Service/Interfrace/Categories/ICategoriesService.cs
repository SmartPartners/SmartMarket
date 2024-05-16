using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Domin.Entities.ContrAgents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Interfrace.Categories
{
    public interface ICategoriesService
    {
        public Task<IList<Category>> GetcategoriesAsync();
    }
}
