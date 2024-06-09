using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.Interfaces;

public interface IProductsStore
{
    List<ProductViewModel> GetProducts();
    ProductViewModel? GetById(int id);
    ProductViewModel Create(ProductViewModel product);
    void Update(ProductViewModel product);
    void Delete(int id);
}
