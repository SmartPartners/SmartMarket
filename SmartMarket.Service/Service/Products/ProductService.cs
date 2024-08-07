﻿using Newtonsoft.Json;
using SmartMarket.Desktop.Service.Interfrace.Product;
using SmartMarket.Desktop.Service.StaticModels;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Service.Products
{
    public class ProductService : IProductService
    {
        public Task<IList<Product>> CreateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Product>> GetProductsAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri($"{StaticModel.BASE_URL}" + $"/api/Products");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenHelper.apiToken}");
                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
                string response = await message.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BaseModelList<Product>>(response);
                return result.Data!;
            }
            catch
            {
                return null;
            }
        }
    }
}
