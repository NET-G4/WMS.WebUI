using Newtonsoft.Json;
using System.Text;
using WMS.WebUI.Models;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores;

public class CategoryStore : ICategoryStore
{
    private readonly HttpClient _client;

    public CategoryStore()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("https://localhost:7097/api/");
    }

    public async Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel category)
    {
        var json = JsonConvert.SerializeObject(category);
        var request = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("categories", request);

        response.EnsureSuccessStatusCode();
        
        var responseJson = await response.Content.ReadAsStringAsync();
        var createdCategory = JsonConvert.DeserializeObject<CategoryViewModel>(responseJson);

        if (createdCategory is null)
        {
            throw new JsonSerializationException("Error serializing Category response from API.");
        }
        
        return createdCategory;
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var response = await _client.DeleteAsync($"categories/{id}");

        response.EnsureSuccessStatusCode();
    }

    public async Task<List<CategoryViewModel>> GetCategoriesAsync(string? search = null)
    {
        var response = await _client.GetAsync($"categories?search={search}");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var categories = JsonConvert.DeserializeObject<PaginatedResponse<CategoryViewModel>>(json);

        if (categories is null)
        {
            throw new JsonSerializationException("Error serializing Category response from API.");
        }

        return categories.Data;
    }

    public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
    {
        var response = await _client.GetAsync($"categories/{id}");
        
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var category = JsonConvert.DeserializeObject<CategoryViewModel>(json);

        if (category is null)
        {
            throw new JsonSerializationException("Error serializing Category response from API.");
        }

        return category;
    }

    public async Task UpdateCategoryAsync(CategoryViewModel category)
    {
        var json = JsonConvert.SerializeObject(category);
        var request = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"categories/{category.Id}", request);

        response.EnsureSuccessStatusCode();
    }

    public async Task<Stream> GetExportFileAsync(DownloadFileType fileType = DownloadFileType.PDF)
    {
        var response = await _client.GetAsync($"categories/download?fileType={fileType}");
        var stream = await response.Content.ReadAsStreamAsync();

        return stream;
    }
}
