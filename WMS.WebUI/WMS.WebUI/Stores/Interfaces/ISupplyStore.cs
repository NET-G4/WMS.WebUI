using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.ViewModels.SupplyViewModels;

namespace WMS.WebUI.Stores.Interfaces;

public interface ISupplyStore
{
    Task<PaginatedApiResponse<SupplyViewModel>> GetSupplies(TransactionQueryParameters queryParameters);
    Task<SupplyViewModel> GetById(int id);
    Task<SupplyViewModel> Create(SupplyViewModel supply);
    Task Update(SupplyViewModel supply);
    Task Delete(int id);
}
