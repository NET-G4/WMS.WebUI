using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Controllers;

public class ProductsController(IProductsStore store,ICategoryStore categoryStore) : Controller
{
    private readonly ICategoryStore _categoryStore = categoryStore;
    private readonly IProductsStore _productsStore = store ?? throw new ArgumentNullException(nameof(store));

    public async Task<IActionResult> Index(
        string? search,
        int? pageNumber,
        int? categoryId,
        decimal? maxPrice,
        decimal? minPrice,
        bool? lowQuantityInStock)
    {
        var products = await _productsStore.GetProducts(new ProductQueryParameters());
        var categories = await _categoryStore.GetCategoriesAsync();

        PopulateViewBag(products, search);

        return View(products.Data);
    }

    private void PopulateViewBag(
        PaginatedApiResponse<ProductViewModel> products,
        string? searchString)
    {
        ViewBag.SearchString = searchString;
        ViewBag.PageSize = products.PageSize;
        ViewBag.TotalPages = products.PagesCount;
        ViewBag.TotalItems = products.TotalCount;
        ViewBag.CurrentPage = products.CurrentPage;
        ViewBag.HasPreviousPage = products.HasPreviousPage;
        ViewBag.HasNextPage = products.HasNextPage;
    }
}
