using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.Mocks;

public class MockProductsStore : IProductsStore
{
    private static int id = 1;
    private static readonly List<ProductViewModel> _products = [];

    public async Task<ProductViewModel> CreateAsync(ProductViewModel product)
    {
        product.Id = id++;
        _products.Add(product);

        return product;
    }

    public async Task DeleteAsync(int id)
    {
        var product = _products.FirstOrDefault(x => x.Id == id);

        if (product is null)
        {
            return;
        }

        var index = _products.IndexOf(product);

        if(index > 0)
        {
            _products.RemoveAt(index);
        }
    }

    public async Task<ProductViewModel?> GetByIdAsync(int id)
    {
        return _products.FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<ProductViewModel>> GetProductsAsync(string? search = null)
    {
        return _products.ToList();
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
