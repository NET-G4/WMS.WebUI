using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductsStore _productsStore;
    public ProductsController(IProductsStore productsStore)
    {
        _productsStore = productsStore;
    }

    // GET: ProductsController
    public async Task<ActionResult> Index(string? searchString = null)
    {
        var products = await _productsStore.GetProductsAsync(searchString);
        ViewBag.SearchString = searchString;

        return View(products);
    }

    // GET: ProductsController/Details/5
    public ActionResult Details(int id)
    {
        var product = _productsStore.GetByIdAsync(id);

        return View();
    }

    // GET: ProductsController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: ProductsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create([Bind("Name, Description")] ProductViewModel product)
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

    // GET: ProductsController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var product = await _productsStore.GetByIdAsync(id);

        if(product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: ProductsController/Edit/5
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

    // GET: ProductsController/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        var product = await _productsStore.GetByIdAsync(id);

        if(product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: ProductsController/Delete/5
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
