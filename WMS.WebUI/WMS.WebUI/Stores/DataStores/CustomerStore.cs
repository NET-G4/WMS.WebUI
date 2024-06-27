using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using WMS.WebUI.Constants;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Services;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels.CustomerViewModels;

namespace WMS.WebUI.Stores.DataStores;

public class CustomerStore(ApiClient apiClient) : ICustomerStore
{
    private readonly ApiClient _apiClient = apiClient;
    public async Task<CustomerDisplayViewModel> Create(CustomerActionViewModel customer)
    {
        var createdCustomer = await _apiClient.PostAsync<CustomerDisplayViewModel
            ,CustomerActionViewModel>(ApiResourceConstants.Customers,customer);

        return createdCustomer;
    }

    public async Task Delete(int id)
    {
        await _apiClient.DeleteAsync(ApiResourceConstants.Customers,id);
    }

    public async Task<CustomerDisplayViewModel> GetById(int id)
    {
        var customer = await _apiClient.GetAsync<CustomerDisplayViewModel>(ApiResourceConstants.Customers + "/" + id);
        return customer;
    }

    public async Task<PaginatedApiResponse<CustomerDisplayViewModel>> GetCustomers(CustomerQueryParameters queryParameters)
    {
        var query = BuildQueryParameters(queryParameters);

        var url = string.IsNullOrEmpty(query) ?
          ApiResourceConstants.Customers :
          ApiResourceConstants.Customers + "?" + query;

        var customers = await _apiClient.GetAsync<PaginatedApiResponse<CustomerDisplayViewModel>>(url);

        return customers;
    }

    public async Task Update(CustomerActionViewModel customer)
    {
        await _apiClient.PutAsync(ApiResourceConstants.Customers, customer);
    }
    private string BuildQueryParameters(CustomerQueryParameters queryParameters)
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
