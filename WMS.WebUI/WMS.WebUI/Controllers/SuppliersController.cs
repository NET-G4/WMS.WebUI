using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Stores.DataStores;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels.SupplierViewModels;

namespace WMS.WebUI.Controllers
{
    public class SuppliersController(ISupplierStore store) : Controller
    {
        private readonly ISupplierStore _supplierStore = store;
        public async Task<IActionResult> Index(
            string? searchString,
            decimal? balanceGreaterThan,
            decimal? balanceLessThan,
            int? pageNumber)
        {
            var queryParameters = new SupplierQueryParameters()
            {
                Search = searchString,
                BalanceGreaterThan = balanceGreaterThan,
                BalanceLessThan = balanceLessThan,
                PageNumber = pageNumber
            };

            var suppliers = await _supplierStore.GetSuppliers(queryParameters);

            PopulateViewBag(suppliers, queryParameters);

            return View(suppliers.Data);
        }
        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _supplierStore.GetById(id);
            return View(supplier);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SupplierActionViewModel supplierActionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(supplierActionViewModel);
            }
            var createdSupplier = await _supplierStore.Create(supplierActionViewModel);

            return RedirectToAction(nameof(Details), new { id = createdSupplier.Id });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierStore.GetById(id);

            var supplierForEdit = ParseSupplierActionViewModel(supplier);

            return View(supplierForEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupplierActionViewModel supplierActionViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(supplierActionViewModel);
            }
            if (supplierActionViewModel.Id != id)
            {
                return BadRequest("Does not match id with supplier id");
            }

            await _supplierStore.Update(supplierActionViewModel);


            return RedirectToAction(nameof(Details), new { id = supplierActionViewModel.Id });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierStore.GetById(id);

            return View(supplier);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _supplierStore.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        private void PopulateViewBag(PaginatedApiResponse<SupplierDisplayViewModel> suppliers, SupplierQueryParameters queryParameters)
        {
            ViewBag.BalanceLessThan = queryParameters.BalanceLessThan;
            ViewBag.BalanceGreaterThan= queryParameters.BalanceGreaterThan;
            ViewBag.SearchString = queryParameters.Search;

            ViewBag.PageSize = suppliers.PageSize;
            ViewBag.TotalPages = suppliers.PagesCount;
            ViewBag.TotalItems = suppliers.TotalCount;
            ViewBag.CurrentPage = suppliers.CurrentPage;
            ViewBag.HasPreviousPage = suppliers.HasPreviousPage;
            ViewBag.HasNextPage = suppliers.HasNextPage;
        }
        private SupplierActionViewModel ParseSupplierActionViewModel(SupplierDisplayViewModel model)
        {
            string[] nameParts = model.FullName.Split(new char[] { ' ' }, 2);

            string firstName = nameParts[0]; // First part is the first name

            // If there's more than one part, the rest is considered the last name
            string lastName = (nameParts.Length > 1) ? nameParts[1] : "";

            var supplierForEdit = new SupplierActionViewModel()
            {
                Id = model.Id,
                FirstName = firstName,
                LastName = lastName,
                Balance = model.Balance,
                PhoneNumber = model.PhoneNumber,
            };
            return supplierForEdit;
        }
    }
}
