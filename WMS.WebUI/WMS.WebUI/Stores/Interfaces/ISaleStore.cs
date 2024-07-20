using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.ViewModels.SaleViewModels;

namespace WMS.WebUI.Stores.Interfaces;
public interface ISaleStore
{
    Task<PaginatedApiResponse<SaleViewModel>> GetSales(TransactionQueryParameters queryParameters);
    Task<SaleViewModel> GetById(int id);
    Task<SaleViewModel> Create(SaleViewModel sale);
    Task Update(SaleViewModel sale);
    Task Delete(int id);
}
