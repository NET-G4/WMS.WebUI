using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.Interfaces;

public interface ICategoryStore
{
    Task<PaginatedApiResponse<CategoryViewModel>> GetCategoriesAsync(string? search = null,int? pageNumber = 1);
    Task<CategoryViewModel> GetCategoryByIdAsync(int id);
    Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel category);
    Task UpdateCategoryAsync(CategoryViewModel category);
    Task DeleteCategoryAsync(int id);
}
