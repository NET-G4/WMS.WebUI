using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.Services;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores;

public class CategoryStore : ICategoryStore
{
    private readonly ApiClient _client;
    private const string RESOURCE = "categories";

    public CategoryStore(ApiClient client)
    {
        _client = client;
    }

    public async Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel category)
    {
        var result = await _client.PostAsync(RESOURCE, category);
        
        return result;
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _client.DeleteAsync(RESOURCE, id);
    }

    public async Task<PaginatedApiResponse<CategoryViewModel>> GetCategoriesAsync(string? search = null, int? pageNumber = 1)
    {
        pageNumber ??= 1;

        var queryParams = $"?search={search}&pageNumber={pageNumber}";
        var result = await _client.GetAsync<PaginatedApiResponse<CategoryViewModel>>(RESOURCE + queryParams);

        return result;
    }

    public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
    {
        var category = await _client.GetAsync<CategoryViewModel>($"categories/{id}");
        
        return category;
    }

    public async Task UpdateCategoryAsync(CategoryViewModel category)
    {
        await _client.PutAsync($"categories/{category.Id}", category);
    }
}
