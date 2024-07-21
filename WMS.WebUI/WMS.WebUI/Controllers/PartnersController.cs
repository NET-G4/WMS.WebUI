using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMS.WebUI.Helpers;
using WMS.WebUI.Stores.Interfaces;
using WMS.WebUI.ViewModels;
using WMS.WebUI.ViewModels.PartnerViewModels;

namespace WMS.WebUI.Controllers;

public class PartnersController : Controller
{
    private readonly IPartnerStore _partnerStore;

    public PartnersController(IPartnerStore partnersStore,
        IProductsStore productsStore,
        IPartnerStore partnerStore)
    {
        _partnerStore=partnerStore;
    }

    public async Task<IActionResult> Index(string? searchString = null, string? partnerType = null)
    {
        var partners = await _partnerStore.GetPartnersAsync(searchString, partnerType);
        
        ViewBag.SelectedType = partnerType ?? "All";

        return View(partners);
    }

    public async Task<IActionResult> Create()
    {
        var partnersTask = await _partnerStore.GetPartnersAsync();


        ViewBag.Types = new string[] { "Customer", "Supplier" };
        ViewBag.SelectedType = "Customer";
        ViewBag.Partners = partnersTask;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePartnerViewModel data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var createdPartner = await _partnerStore.Create(data);
        var result = new
        {
            redirectToUrl = Url.Action(
                "Details",
                new
                {
                    id = createdPartner.Id,
                    type = (int)createdPartner.Type
                })
        };

        return Json(result);
    }

    public async Task<IActionResult> Details(int id, PartnerType type)
    {
        var partner = await _partnerStore.GetByIdAndTypeAsync(id, type);

        return View(partner);
    }
}
