using System.Text;
using WMS.WebUI.Constants;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Services;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.DataStores;

public class ProductStore : IProductsStore
{
    private readonly ApiClient _client;

    public ProductStore(ApiClient client)
    {
        _client = client;
    }

    public async Task<ProductViewModel> Create(ProductViewModel product)
    {
        var result = await _client.PostAsync<ProductViewModel,ProductViewModel>(ApiResourceConstants.Products, product);

        return result;  
    }

    public async Task Delete(int id)
    {
        await _client.DeleteAsync(ApiResourceConstants.Products, id);
    }

    public async Task<PaginatedApiResponse<ProductViewModel>> GetProducts(ProductQueryParameters queryParameters)
    {
        var queries = BuildQueryParameters(queryParameters);
        var url = string.IsNullOrEmpty(queries) ?
            ApiResourceConstants.Products :
            ApiResourceConstants.Products + "?" + queries;

        var result = await _client.GetAsync<PaginatedApiResponse<ProductViewModel>>(url);

        return result;
    }

    public async Task<ProductViewModel> GetById(int id)
    {
        var result = await _client.GetAsync<ProductViewModel>(ApiResourceConstants.Products + "/" + id);

        return result;
    }

    public async Task Update(ProductViewModel product)
    {
        await _client.PutAsync(ApiResourceConstants.Products + "/" + product.Id, product);
    }

    private static string BuildQueryParameters(ProductQueryParameters queryParameters)
    {
        StringBuilder queryBuilder = new();

        if (queryParameters.PageNumber.HasValue)
        {
            queryBuilder.Append($"pageNumber={queryParameters.PageNumber}&");
        }
        else
        {
            queryBuilder.Append($"pageNumber=1&");
        }

        if (!string.IsNullOrWhiteSpace(queryParameters.Search))
        {
            queryBuilder.Append($"Search={queryParameters.Search}&");
        }

        if (queryParameters.CategoryId.HasValue)
        {
            queryBuilder.Append($"CategoryId={queryParameters.CategoryId}&");
        }

        if (queryParameters.MinPrice.HasValue)
        {
            queryBuilder.Append($"MinPrice={queryParameters.MinPrice}&");
        }

        if (queryParameters.MaxPrice.HasValue)
        {
            queryBuilder.Append($"MaxPrice={queryParameters.MaxPrice}&");
        }

        if (queryParameters.IsLowQuantity.HasValue && queryParameters.IsLowQuantity == true)
        {
            queryBuilder = queryBuilder.Append($"IsLowQuantity={queryParameters.IsLowQuantity}");
        }

        return queryBuilder.ToString();
    }
}
