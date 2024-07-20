using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels.SaleViewModels;

namespace WMS.WebUI.Controllers;

public class SalesController : Controller
{
    private readonly ICustomerStore _customerStore;
    private readonly ISaleStore _salesStore;

    public SalesController(ISaleStore saleStore, ICustomerStore customerStore)
    {
        _customerStore = customerStore;
        _salesStore = saleStore;
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

        var sales = await _salesStore.GetSales(queryParameters);

        PopulateViewBag(sales, queryParameters);

        return View(sales.Data);
    }

    public async Task<IActionResult> Details(int id)
    {
        var sale = await _salesStore.GetById(id);
        return View(sale);
    }

    private void PopulateViewBag(PaginatedApiResponse<SaleViewModel> sales, TransactionQueryParameters queryParameters)
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
