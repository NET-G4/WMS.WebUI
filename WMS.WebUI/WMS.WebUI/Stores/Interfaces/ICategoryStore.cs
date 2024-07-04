using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Stores.Interfaces;

public interface ICategoryStore
{
    Task<List<CategoryViewModel>> GetCategoriesAsync(string? search = null);
    Task<CategoryViewModel> GetCategoryByIdAsync(int id);
    Task<CategoryViewModel> CreateCategoryAsync(CategoryViewModel category);
    Task UpdateCategoryAsync(CategoryViewModel category);
    Task DeleteCategoryAsync(int id);
    Task<Stream> GetExportFileAsync(DownloadFileType fileType = DownloadFileType.PDF);
}

public enum DownloadFileType
{
    PDF,
    EXCEL
}
