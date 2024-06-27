using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WMS.WebUI.Models.PaginatedResponse;
using WMS.WebUI.QueryParams;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels.CustomerViewModels;

namespace WMS.WebUI.Controllers;

public class CustomersController(ICustomerStore customerStore) : Controller
{
    private readonly ICustomerStore _customerStore = customerStore;
    public async Task<IActionResult> Index(
        string? searchString,
        decimal? balanceGreaterThan,
        decimal? balanceLessThan,
        int? pageNumber)
    {
        var queryParameters = new CustomerQueryParameters()
        {
            Search = searchString,
            BalanceGreaterThan = balanceGreaterThan,
            BalanceLessThan = balanceLessThan,
            PageNumber = pageNumber
        };

        var customers = await _customerStore.GetCustomers(queryParameters);

        PopulateViewBag(customers, queryParameters);

        return View(customers.Data);
    }
    public async Task<IActionResult> Details(int id)
    {
        var customer = await _customerStore.GetById(id);
        return View(customer);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CustomerActionViewModel customerActionViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(customerActionViewModel);
        }
        var createdCustomer = await _customerStore.Create(customerActionViewModel);

        return RedirectToAction(nameof(Details), new { id = createdCustomer.Id });
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var customer = await _customerStore.GetById(id);

        var customerForEdit = ParseCustomerActionViewModel(customer);

        return View(customerForEdit);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,CustomerActionViewModel customerActionViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(customerActionViewModel);
        } 
        if(customerActionViewModel.Id != id )
        {
            return BadRequest("Does not match id with customer id");
        }

        await _customerStore.Update(customerActionViewModel);


        return RedirectToAction(nameof(Details),new {id = customerActionViewModel.Id});
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _customerStore.GetById(id);

        return View(customer);
    }
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken] 
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _customerStore.Delete(id);
        return RedirectToAction(nameof(Index));
    }
    private void PopulateViewBag(PaginatedApiResponse<CustomerDisplayViewModel> customers,CustomerQueryParameters queryParameters)
    {
        ViewBag.BalanceLessThan = queryParameters.BalanceLessThan;
        ViewBag.BalanceGreaterThan= queryParameters.BalanceGreaterThan;
        ViewBag.SearchString = queryParameters.Search;

        ViewBag.PageSize = customers.PageSize;
        ViewBag.TotalPages = customers.PagesCount;
        ViewBag.TotalItems = customers.TotalCount;
        ViewBag.CurrentPage = customers.CurrentPage;
        ViewBag.HasPreviousPage = customers.HasPreviousPage;
        ViewBag.HasNextPage = customers.HasNextPage;
    }
    private CustomerActionViewModel ParseCustomerActionViewModel(CustomerDisplayViewModel model)
    {
        string[] nameParts = model.FullName.Split(new char[] { ' ' }, 2);

        string firstName = nameParts[0]; // First part is the first name

        // If there's more than one part, the rest is considered the last name
        string lastName = (nameParts.Length > 1) ? nameParts[1] : "";

        var customerForEdit = new CustomerActionViewModel()
        {
            Id = model.Id,
            FirstName = firstName,
            LastName = lastName,
            Balance = model.Balance,
            Address = model.Address,
            Discount = model.Discount,
            PhoneNumber = model.PhoneNumber,
        };
        return customerForEdit;
    }
    
}
