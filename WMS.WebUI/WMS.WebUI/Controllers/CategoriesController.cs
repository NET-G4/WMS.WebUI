using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryStore _categoryStore;

    public CategoriesController(ICategoryStore categoryStore)
    {
        _categoryStore = categoryStore;
    }

    // GET: Categories
    public async Task<ActionResult> Index(string? searchString)
    {
        var categories = await _categoryStore.GetCategoriesAsync(searchString);

        ViewBag.SearchString = searchString;

        return View(categories);
    }

    public async Task<IActionResult> Download()
    {
        var stream = await _categoryStore.GetExportFileAsync();

        return File(stream, "application/pdf", "categories.pdf");
    }

    // GET: Categories/Details/5
    public async Task<ActionResult> Details(int id)
    {
        var category = await _categoryStore.GetCategoryByIdAsync(id);

        return View(category);
    }

    // GET: Categories/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind("Name,Description")] CategoryViewModel category)
    {
        try
        {
            var createdCategory = await _categoryStore.CreateCategoryAsync(category);

            return RedirectToAction(nameof(Details), new { id = createdCategory.Id });
        }
        catch
        {
            return View();
        }
    }

    // GET: Categories/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var category = await _categoryStore.GetCategoryByIdAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Categories/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, [Bind("Id,Name,Description")] CategoryViewModel category)
    {
        try
        {
            await _categoryStore.UpdateCategoryAsync(category);

            return RedirectToAction(nameof(Details), new { id });
        }
        catch
        {
            return View();
        }
    }

    // GET: Categories/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        var category = await _categoryStore.GetCategoryByIdAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ConfirmDelete(int id)
    {
        try
        {
            await _categoryStore.DeleteCategoryAsync(id);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
