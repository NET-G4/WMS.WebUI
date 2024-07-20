using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.Models.Adjustment;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels.SupplyViewModels;

namespace WMS.WebUI.Controllers;

public class AdjustmentController(ISupplyStore supplyStore,ISaleStore saleStore) : Controller
{
    private readonly ISaleStore _saleStore = saleStore;
    private readonly ISupplyStore _supplyStore = supplyStore;
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
        var sales = await _saleStore.GetSales(queryParameters);

        var transactions = new Transactions()
        {
            Sales = sales.Data,
            Supplies = supplies.Data
        };

      //  PopulateViewBag(transactions, queryParameters);

        return View(supplies.Data);
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
