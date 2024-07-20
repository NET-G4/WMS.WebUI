using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.Helpers;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

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
        ViewBag.SelectedType = "Sale";
        ViewBag.Partners = partnersTask.Result;
        ViewBag.Products= productsTask.Result.Data;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTransactionViewModel data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var createdTransaction = await _transactionsStore.Create(data);
        var result = new 
        { 
            redirectToUrl = Url.Action(
                "Details", 
                new 
                { 
                    id = createdTransaction.Id, 
                    type = (int)createdTransaction.Type
                })
        };

        return Json(result);
    }

    public async Task<IActionResult> Details(int id, TransactionType type)
    {
        var transaction = await _transactionsStore.GetByIdAndTypeAsync(id, type);

        return View(transaction);
    }
}
