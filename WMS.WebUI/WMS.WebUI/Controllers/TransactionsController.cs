using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.Helpers;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;

namespace WMS.WebUI.Controllers;

public class TransactionsController : Controller
{
    private readonly ITransactionsStore _transactionsStore;
    private readonly IProductsStore _productsStore;
    private readonly IPartnerStore _partnerStore;

    public TransactionsController(ITransactionsStore transactionsStore, 
        IProductsStore productsStore,
        IPartnerStore partnerStore)
    {
        _transactionsStore = Validator.NotNull(transactionsStore);
        _productsStore = Validator.NotNull(productsStore);
        _partnerStore=partnerStore;
    }

    public async Task<IActionResult> Index(string? searchString = null, string? transactionType = null)
    {
        var transactions = await _transactionsStore.GetTransactionsAsync(searchString, transactionType);

        ViewBag.SelectedType = transactionType ?? "All";

        return View(transactions);
    }
    public async Task<IActionResult> Details(int id, TransactionType type)
    {
        var transaction = await _transactionsStore.GetByIdAndTypeAsync(id, type);

        return View(transaction);
    }

    public async Task<IActionResult> Create()
    {
        var partnersTask = _partnerStore.GetPartnersAsync();
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

        var createdTransaction = await _transactionsStore.CreateAsync(data);
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
    public async Task<IActionResult> Edit(int id,TransactionType type)
    {
        var partnersTask = _partnerStore.GetPartnersAsync();
        var productsTask = _productsStore.GetProductsAsync();
        var transaction = _transactionsStore.GetByIdAndTypeAsync(id, type);

        await Task.WhenAll(partnersTask, productsTask,transaction);

        ViewBag.Types = new string[] { "Sale", "Supply" };
        ViewBag.SelectedType = "Sale";
        ViewBag.Partners = partnersTask.Result;
        ViewBag.Products= productsTask.Result.Data;

        return View(transaction.Result);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        [FromBody] TransactionView data)
    {
        if (!ModelState.IsValid)
        {
            return View(data);
        }
        await _transactionsStore.UpdateAsync(data);
        
        var result = new
        {
            redirectToUrl = Url.Action(
                "Details",
                new
                {
                    id = data.Id,
                    type = (int)data.Type
                })
        };

        return Json(result);
    }
    public async Task<IActionResult> Delete(int id, TransactionType type)
    {
        var transaction = await _transactionsStore.GetByIdAndTypeAsync(id, type);

        return View(transaction);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> ConfirmDelete(int id, TransactionType type)
    {
        await _transactionsStore.DeleteAsync(id, type);

        return RedirectToAction(nameof(Index));
    }

}
