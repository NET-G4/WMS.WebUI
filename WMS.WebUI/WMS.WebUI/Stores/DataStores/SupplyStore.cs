using System.Text;
using WMS.WebUI.Constants;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Services;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels.SupplyViewModels;

namespace WMS.WebUI.Stores.DataStores;

public class SupplyStore(ApiClient apiClient) : ISupplyStore
{
    private readonly ApiClient _apiClient = apiClient;
    public async Task<PaginatedApiResponse<SupplyViewModel>> GetSupplies(TransactionQueryParameters queryParameters)
    {
        var query = BuildQueryParameters(queryParameters);
        var supplies = await _apiClient.GetAsync<PaginatedApiResponse<SupplyViewModel>>(ApiResourceConstants.Supplies + "?" + query);
        return supplies;
    }
    public async Task<SupplyViewModel> GetById(int id)
    {
        var supply = await _apiClient.GetAsync<SupplyViewModel>(ApiResourceConstants.Supplies + "/" + id);
        return supply;
    }

    public Task<SupplyViewModel> Create(SupplyViewModel supply)
    {
        var createdSupply = _apiClient.PostAsync<SupplyViewModel, SupplyViewModel>(ApiResourceConstants.Supplies, supply);
        return createdSupply;
    }
    public async Task Update(SupplyViewModel supply)
    {
        await _apiClient.PutAsync<SupplyViewModel>(ApiResourceConstants.Supplies, supply);
    }

    public async Task Delete(int id)
    {
        await _apiClient.DeleteAsync(ApiResourceConstants.Supplies, id);
    }
    private string BuildQueryParameters(TransactionQueryParameters queryParameters)
    {
        StringBuilder query = new StringBuilder();

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
        if (queryParameters.FromDate.HasValue)
        {
            query.Append($"FromDate={queryParameters.FromDate.Value:yyyy-MM-ddTHH:mm:ss.fffZ}&");
        }
        if (queryParameters.ToDate.HasValue)
        {
            query.Append($"ToDate={queryParameters.ToDate.Value:yyyy-MM-ddTHH:mm:ss.fffZ}&");
        }
        return query.ToString();
    }
}
