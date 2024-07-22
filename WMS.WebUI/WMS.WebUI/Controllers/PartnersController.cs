using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using WMS.WebUI.Helpers;
using WMS.WebUI.Stores;
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
        ViewBag.Types = new string[] { "Customer", "Supplier" };
        ViewBag.SelectedType = "Customer";

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePartnerViewModel data)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var createdPartner = await _partnerStore.Create(data);
        var result = new
        {
            redirectToUrl = Url.Action(
                "Details", "Partners",
                new
                {
                    id = createdPartner.Id,
                    type = (int)createdPartner.Type
                })
        };

        return Json(result);
    }


    public async Task<IActionResult> Details(int id, int type)
    {
        var partner = await _partnerStore.GetByIdAndTypeAsync(id, (PartnerType)type);

        return View(partner);
    }
    public async Task<IActionResult> Edit(int id, int type)
    {
        var partner = await _partnerStore.GetByIdAndTypeAsync(id, (PartnerType)type);

        return View(partner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, [Bind("id, Name, Description")] EditPartnerViewModel partner)
    {
        try
        {
            await _partnerStore.UpdateAsync(partner);

            return RedirectToAction(nameof(Details), new { id });
        }
        catch
        {
            return View(partner);
        }
    }
    public async Task<IActionResult> Delete(int id, int type)
    {
        var partner = await _partnerStore.GetByIdAndTypeAsync(id, (PartnerType)type);

        return View(partner);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmDelete(int id, int type)
    {
        try
        {
            await _partnerStore.Delete(id, (PartnerType)type);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View(); 
        }
    }
}
