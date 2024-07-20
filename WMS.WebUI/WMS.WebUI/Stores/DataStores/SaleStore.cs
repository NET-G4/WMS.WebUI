using Syncfusion.EJ2.Diagrams;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using WMS.WebUI.Constants;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Services;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.SaleViewModels;

namespace WMS.WebUI.Stores.DataStores;

public class SaleStore(ApiClient apiClient) : ISaleStore
{
    private readonly ApiClient _apiClient = apiClient;
    public async Task<PaginatedApiResponse<SaleViewModel>> GetSales(TransactionQueryParameters queryParameters)
    {
        var query = BuildQueryParameters(queryParameters);
        var sales = await _apiClient.GetAsync<PaginatedApiResponse<SaleViewModel>>(ApiResourceConstants.Sales + "?" + query);
        return sales;
    }
    public async Task<SaleViewModel> GetById(int id)
    {
        var sale = await _apiClient.GetAsync<SaleViewModel>(ApiResourceConstants.Sales + "/" + id);
        return sale;
    }

    public async Task<TransactionView> Create(CreateTransactionViewModel transaction)
    {
        var transactionNew = new TransactionView();

        return transactionNew;
    }
    public async Task Update(SaleViewModel sale)
    {
        await _apiClient.PutAsync<SaleViewModel>(ApiResourceConstants.Sales, sale);
    }

    public async Task Delete(int id)
    {
        await _apiClient.DeleteAsync(ApiResourceConstants.Sales, id);
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
