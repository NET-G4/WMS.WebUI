using WMS.WebUI.Models;
using WMS.WebUI.ViewModels;
namespace WMS.WebUI.Stores.Interfaces;

public interface IProductsStore
{
    Task<PaginatedResponse<ProductViewModel>> GetProductsAsync(string? search = null, int? categoryId = null);
    Task<ProductViewModel?> GetByIdAsync(int id);
    Task<ProductViewModel> CreateAsync(ProductViewModel product);
    Task UpdateAsync(ProductViewModel product);
    Task DeleteAsync(int id);
}
