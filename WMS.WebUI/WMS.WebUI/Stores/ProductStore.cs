using Newtonsoft.Json;
using System.Text;
using WMS.WebUI.Models;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores
{
    public class ProductStore : IProductsStore
    {
        private readonly HttpClient _httpClient;
        
        public ProductStore()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7097/api/");
        }

        public async Task<PaginatedResponse<ProductViewModel>> GetProductsAsync(string? search = null, int? categoryId = null)
        {
            var response = await _httpClient.GetAsync($"products?search={search}&categoryId={categoryId}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<PaginatedResponse<ProductViewModel>>(json);

            if (product is null)
            {
                throw new JsonSerializationException("Error serializing Product response from API.");
            }

            return product;
        }
        
        public async Task<ProductViewModel?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"products/{id}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductViewModel>(json);

            if (product is null)
            {
                throw new JsonSerializationException("Error serializing Product response from API.");
            }

            return product;
        }

        public async Task UpdateAsync(ProductViewModel product)
        {
            var json = JsonConvert.SerializeObject(product);
            var request = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"products/{product.Id}", request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<ProductViewModel> CreateAsync(ProductViewModel product)
        {
            var json = JsonConvert.SerializeObject(product);
            var request = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("products", request);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var createdProduct = JsonConvert.DeserializeObject<ProductViewModel>(responseJson);

            if(createdProduct is null)
            {
                throw new JsonSerializationException("Error serializing Product response from API.");
            }

            return createdProduct;
        }
        
        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");

            response.EnsureSuccessStatusCode();
        }   
    }
}
