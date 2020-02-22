using CTS.Api.Client.Interface;
using CTS.Models.Product;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CTS.Api.Client
{
    public class ProductClient : IProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductInventoryModel>> GetProducts()
        {
            var url = $"/api/Product/GetProductData";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<ProductInventoryModel>>(content);

            return data;
        }

        public async Task<bool> UpdateProductQuantity(ProductInventoryUpdateModel updateModel)
        {
            var url = $"/api/Product/UpdateProductInventory";

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateModel), Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync(url, stringContent).Result;

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var receivedData = JsonConvert.DeserializeObject<bool>(responseString, new JsonSerializerSettings());

            return receivedData;
        }
    }
}
