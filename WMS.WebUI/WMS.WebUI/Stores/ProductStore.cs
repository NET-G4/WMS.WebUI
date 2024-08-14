using WMS.WebUI.Models;
using WMS.WebUI.Services;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores;

public class ProductStore : IProductsStore
{
    private readonly ApiClient _client;
    
    public ProductStore(ApiClient client)
    {
        _client = client;
    }

    public async Task<PaginatedResponse<ProductViewModel>> GetProductsAsync(string? search = null, int? categoryId = null)
    {
        var query = $"products?search={search}&categoryId={categoryId}";
        var products = await _client.GetAsync<PaginatedResponse<ProductViewModel>>(query);

        return products;
    }
    
    public async Task<ProductViewModel?> GetByIdAsync(int id)
    {
        var product = await _client.GetAsync<ProductViewModel>($"products/{id}");

        return product;
    }

    public async Task UpdateAsync(ProductViewModel product)
    {
        await _client.PutAsync($"products/{product.Id}", product);
    }

    public async Task<ProductViewModel> CreateAsync(ProductViewModel product)
    {
        var createdProduct = await _client.PostAsync<ProductViewModel, ProductViewModel>("products", product);

        return createdProduct;
    }
    
    public async Task DeleteAsync(int id)
    {
        await _client.DeleteAsync($"products", id);
    }   
}
