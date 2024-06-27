using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.ViewModels.SupplierViewModels;

namespace WMS.WebUI.Stores.Interfaces;

public interface ISupplierStore
{
    Task<PaginatedApiResponse<SupplierDisplayViewModel>> GetSuppliers(SupplierQueryParameters queryParameters);
    Task<SupplierDisplayViewModel> GetById(int id);
    Task<SupplierDisplayViewModel> Create(SupplierActionViewModel supplier);
    Task Update(SupplierActionViewModel supplier);
    Task Delete(int id);
}
