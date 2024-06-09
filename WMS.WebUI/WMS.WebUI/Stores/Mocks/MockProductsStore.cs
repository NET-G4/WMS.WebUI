using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.Mocks;

public class MockProductsStore : IProductsStore
{
    private static int id = 1;
    private static readonly List<ProductViewModel> _products = [];

    public ProductViewModel Create(ProductViewModel product)
    {
        product.Id = id++;
        _products.Add(product);

        return product;
    }

    public void Delete(int id)
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

    public ProductViewModel? GetById(int id)
    {
        return _products.FirstOrDefault(x => x.Id == id);
    }

    public List<ProductViewModel> GetProducts()
    {
        return _products.ToList();
    }

    public void Update(ProductViewModel product)
    {
        var productToUpdate = _products.FirstOrDefault(x => x.Id == product.Id);
        var index = _products.IndexOf(product);

        if (index > 0)
        {
            _products[index] = product;
        }
    }
}
