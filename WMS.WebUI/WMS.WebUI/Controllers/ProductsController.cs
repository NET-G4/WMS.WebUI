using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WMS.WebUI.Helpers;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductsStore _productsStore;
    private readonly ICategoryStore _categoryStore;

    public ProductsController(IProductsStore productsStore, ICategoryStore categoryStore)
    {
        _productsStore = Validator.NotNull(productsStore);
        _categoryStore = Validator.NotNull(categoryStore);
    }

    public async Task<ActionResult> Index(string? searchString = null, int? categoryId = null)
    {
        var productsTask =  _productsStore.GetProductsAsync(searchString, categoryId);
        var categoriesTask = _categoryStore.GetCategoriesAsync();

        await Task.WhenAll(productsTask, categoriesTask);

        ViewBag.SearchString = searchString;
        ViewBag.Categories = categoriesTask.Result;
        ViewBag.SelectedCategoryId = categoryId;

        return View(productsTask.Result.Data);
    }

    public async Task<ActionResult> Details(int id)
    {
        var product = await _productsStore.GetByIdAsync(id);

        return View(product);
    }

    public async Task<ActionResult> Create()
    {
        var categories = await _categoryStore.GetCategoriesAsync();
        ViewBag.Categories = categories;
        ViewBag.CategoryId = categories.FirstOrDefault();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind("Name,Description,SupplyPrice,SalePrice,QuantityInStock,LowQuantityAmount,CategoryId")] ProductViewModel product)
    {
        try
        {
            var createdProduct = await _productsStore.CreateAsync(product);

            return RedirectToAction(nameof(Details), new { id = createdProduct.Id });
        }
        catch
        {
            return View();
        }
    }

    public async Task<ActionResult> Edit(int id)
    {
        var productTask = _productsStore.GetByIdAsync(id);
        var categoriesTask = _categoryStore.GetCategoriesAsync();

        await Task.WhenAll(productTask, categoriesTask);

        var product = productTask.Result;

        if(product is null)
        {
            return NotFound();
        }

        ViewBag.Categories = categoriesTask.Result;

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, [Bind("id, Name, Description")] ProductViewModel product)
    {
        try
        {
            await _productsStore.UpdateAsync(product);

            return RedirectToAction(nameof(Details), new { id });
        }
        catch
        {
            return View();
        }
    }

    public async Task<ActionResult> Delete(int id)
    {
        var product = await _productsStore.GetByIdAsync(id);

        if(product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ConfirmDelete(int id)
    {
        try
        {
            await _productsStore.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
