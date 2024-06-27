using System.Text;
using WMS.WebUI.Constants;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Services;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels.SupplierViewModels;

namespace WMS.WebUI.Stores.DataStores;

public class SupplierStore(ApiClient apiClient) : ISupplierStore
{
    private readonly ApiClient _apiClient = apiClient;
    public async Task<SupplierDisplayViewModel> Create(SupplierActionViewModel supplier)
    {
        var createdSupplier = await _apiClient.PostAsync<SupplierDisplayViewModel
            ,SupplierActionViewModel>(ApiResourceConstants.Suppliers, supplier);

        return createdSupplier;
    }

    public async Task Delete(int id)
    {
        await _apiClient.DeleteAsync(ApiResourceConstants.Suppliers,id);
    }

    public async Task<SupplierDisplayViewModel> GetById(int id)
    {
        var supplier = await _apiClient.GetAsync<SupplierDisplayViewModel>(ApiResourceConstants.Suppliers + "/" + id);
        return supplier;
    }

    public async Task<PaginatedApiResponse<SupplierDisplayViewModel>> GetSuppliers(SupplierQueryParameters queryParameters)
    {
        var query = BuildQueryParameters(queryParameters);

        var url = string.IsNullOrEmpty(query) ?
          ApiResourceConstants.Suppliers :
          ApiResourceConstants.Suppliers + "?" + query;

        var suppliers = await _apiClient.GetAsync<PaginatedApiResponse<SupplierDisplayViewModel>>(url);

        return suppliers;
    }

    public async Task Update(SupplierActionViewModel supplier)
    {
        await _apiClient.PutAsync(ApiResourceConstants.Suppliers, supplier);
    }
    private string BuildQueryParameters(SupplierQueryParameters queryParameters)
    {
        var query = new StringBuilder();
        if (queryParameters.PageNumber.HasValue)
        {
            query.Append($"pageNumber={queryParameters.PageNumber}&");
        }
        else
        {
            query.Append($"pageNumber=1&");
        }
        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            query.Append($"Search={queryParameters.Search}&");
        }
        if (queryParameters.BalanceGreaterThan.HasValue)
        {
            query.Append($"BalanceGreaterThan={queryParameters.BalanceGreaterThan}&");
        }
        if (queryParameters.BalanceLessThan.HasValue)
        {
            query.Append($"BalanceLessThan={queryParameters.BalanceLessThan}&");
        }

        return query.ToString();
    }
}
