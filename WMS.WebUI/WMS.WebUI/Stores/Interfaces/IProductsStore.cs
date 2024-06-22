using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.Interfaces;

public interface IProductsStore
{
    Task<PaginatedApiResponse<ProductViewModel>> GetProducts(ProductQueryParameters queryParameters);
    Task<ProductViewModel>? GetById(int id);
    Task<ProductViewModel> Create(ProductViewModel product);
    Task Update(ProductViewModel product);
    Task Delete(int id);
}
