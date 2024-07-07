using WMS.WebUI.Models;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.Mocks;

public class MockProductsStore : IProductsStore
{
    private static int id = 1;
    private static readonly List<ProductViewModel> _products = [];

    public async Task<ProductViewModel> CreateAsync(ProductViewModel product)
    {
        await Task.Delay(100);

        product.Id = id++;
        _products.Add(product);

        return product;
    }

    public Task DeleteAsync(int id)
    {
        var product = _products.FirstOrDefault(x => x.Id == id);

        if (product is null)
        {
            return Task.CompletedTask;
        }

        var index = _products.IndexOf(product);

        if(index > 0)
        {
            _products.RemoveAt(index);
        }

        return Task.CompletedTask;
    }

    public async Task<ProductViewModel?> GetByIdAsync(int id)
    {
        return _products.FirstOrDefault(x => x.Id == id);
    }

    public async Task<PaginatedResponse<ProductViewModel>> GetProductsAsync(string? search = null, int? categoryId = null)
    {
        return new PaginatedResponse<ProductViewModel>();
    }

    public async Task UpdateAsync(ProductViewModel product)
    {
        var productToUpdate = _products.FirstOrDefault(x => x.Id == product.Id);
        var index = _products.IndexOf(product);

        if (index > 0)
        {
            _products[index] = product;
        }
    }
}
