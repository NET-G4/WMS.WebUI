using Microsoft.AspNetCore.Http.HttpResults;
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
        string? searchString,
        int? pageNumber,
        int? categoryId,
        decimal? maxPrice,
        decimal? minPrice,
        string? isLowQuantity 
        )
    {
        var queryParameters = new ProductQueryParameters()
        {
            Search = searchString,
            PageNumber = pageNumber,
            CategoryId = categoryId,
            MaxPrice = maxPrice,
            MinPrice = minPrice,
            IsLowQuantity = isLowQuantity == "on",
        };

        var products = await _productsStore.GetProducts(queryParameters);
        //Problem CAtegories get with pagination with max pagesize=15, I dont choose 16 category
        var categories = await _categoryStore.GetCategoriesAsync();

        ViewBag.Categories = new SelectList(categories.Data.OrderBy(x => x.Name), "Id", "Name", categoryId);
        ViewBag.MinPrice = minPrice;
        ViewBag.IsLowQuantity = isLowQuantity == "on";
        ViewBag.MaxPrice = maxPrice;
        ViewBag.SearchString = searchString;

        PopulateViewBag(products, searchString);

        return View(products.Data);
    }
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productsStore.GetById(id);
        
        if(product is null)
            return NotFound();

        return View(product);
    }
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryStore.GetCategoriesAsync();

        ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");
        
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductViewModel product)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryStore.GetCategoriesAsync();

            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name", categories);
            return View(product);
        }
        var createdProduct = await _productsStore.Create(product);
        return RedirectToAction(nameof(Details),new { id = createdProduct.Id });
    }
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productsStore.GetById(id);

        if (product is null)
            return NotFound();

        var categories = await _categoryStore.GetCategoriesAsync();

        ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");

        return View(product);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,ProductViewModel product)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryStore.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.Data, "Id", "Name");

            return View(product);
        }

        if (product.Id != id)
        {
            return BadRequest("Does not match id with product id");
        }
        await _productsStore.Update(product);
        return RedirectToAction(nameof(Details),new { id = product.Id });
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productsStore.GetById(id);

        if(product is null)
            return NotFound();

        return View(product);
    }
    [HttpPost(),ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _productsStore.GetById(id);

        if (product is null)
            return NotFound();

        await _productsStore.Delete(id);

        return RedirectToAction(nameof(Index));
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
