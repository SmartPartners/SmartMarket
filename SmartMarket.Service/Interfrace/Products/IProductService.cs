using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Interfrace.Product
{
    public interface IProductService
    {
        public Task<IList<SmartMarket.Domin.Entities.Products.Product>> GetProductsAsync();
        public Task<IList<SmartMarket.Domin.Entities.Products.Product>> CreateAsync();
    }
}
