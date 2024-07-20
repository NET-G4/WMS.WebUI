using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels.SupplyViewModels;

namespace WMS.WebUI.Controllers;

public class SuppliesController : Controller
{
    private readonly ICustomerStore _customerStore;
    private readonly ISupplyStore _supplyStore;

    public SuppliesController(ISupplyStore supplyStore, ICustomerStore customerStore)
    {
        _customerStore = customerStore;
         _supplyStore = supplyStore;
    }

    public async Task<IActionResult> Index(string? searchString, DateTime? fromDate, DateTime? toDate, int? pageNumber)
    {
        var queryParameters = new TransactionQueryParameters()
        {
            Search = searchString,
            FromDate = fromDate,
            ToDate = toDate,
            PageNumber = pageNumber
        };

        var supplies = await _supplyStore.GetSupplies(queryParameters);

        PopulateViewBag(supplies, queryParameters);

        return View(supplies.Data);
    }

    public async Task<IActionResult> Details(int id)
    {
        var supply = await _supplyStore.GetById(id);
        return View(supply);
    }

    private void PopulateViewBag(PaginatedApiResponse<SupplyViewModel> sales, TransactionQueryParameters queryParameters)
    {
        ViewBag.FromDate = queryParameters.FromDate?.ToString("yyyy-MM-dd");
        ViewBag.ToDate = queryParameters.ToDate?.ToString("yyyy-MM-dd");
        ViewBag.SearchString = queryParameters.Search;

        ViewBag.PageSize = sales.PageSize;
        ViewBag.TotalPages = sales.PagesCount;
        ViewBag.TotalItems = sales.TotalCount;
        ViewBag.CurrentPage = sales.CurrentPage;
        ViewBag.HasPreviousPage = sales.HasPreviousPage;
        ViewBag.HasNextPage = sales.HasNextPage;
    }
}
