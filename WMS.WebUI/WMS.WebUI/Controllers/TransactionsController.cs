using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.Helpers;
using WMS.WebUI.Stores.Interfaces;

namespace WMS.WebUI.Controllers;

public class TransactionsController : Controller
{
    private readonly ITransactionsStore _transactionsStore;
    private readonly IProductsStore _productsStore;

    public TransactionsController(ITransactionsStore transactionsStore, IProductsStore productsStore)
    {
        _transactionsStore = Validator.NotNull(transactionsStore);
        _productsStore = Validator.NotNull(productsStore);
    }

    public async Task<IActionResult> Index(string? searchString = null, string? transactionType = null)
    {
        var transactions = await _transactionsStore.GetTransactionsAsync(searchString, transactionType);

        ViewBag.SelectedType = transactionType ?? "All";

        return View(transactions);
    }

    public async Task<IActionResult> Create()
    {
        var partnersTask = _transactionsStore.GetPartnersAsync();
        var productsTask = _productsStore.GetProductsAsync();

        await Task.WhenAll(partnersTask, productsTask);

        ViewBag.Types = new string[] { "Sale", "Supply" };
        ViewBag.Partners = partnersTask.Result;
        ViewBag.Products= productsTask.Result.Data;

        return View();
    }
}
