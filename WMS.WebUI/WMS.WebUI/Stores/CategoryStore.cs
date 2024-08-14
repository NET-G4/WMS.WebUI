using WMS.WebUI.Models;
using WMS.WebUI.Services;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores;

public class CategoryStore : ICategoryStore
{
    private const string URL = "categories";
    private readonly ApiClient _client;

    public CategoryStore(ApiClient client)
    {
        _client = client;
    }

    public async Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel category)
    {
        var createdCategory = await _client
            .PostAsync<CategoryViewModel, CategoryViewModel>(URL, category);
        
        return createdCategory;
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _client.DeleteAsync(URL, id);
    }

    public async Task<List<CategoryViewModel>> GetCategoriesAsync(string? search = null)
    {
        var response = await _client.GetAsync<PaginatedResponse<CategoryViewModel>>($"{URL}?search={search}");

        return response.Data;
    }

    public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
    {
        var category = await _client.GetAsync<CategoryViewModel>($"{URL}/{id}");

        return category;
    }

    public async Task UpdateCategoryAsync(CategoryViewModel category)
    {
        await _client.PutAsync(URL, category);
    }

    public async Task<Stream> GetExportFileAsync(DownloadFileType fileType = DownloadFileType.PDF)
    {
        var stream = await _client.GetAsStreamAsync($"{URL}/download?fileType={fileType}");

        return stream;
    }
}
