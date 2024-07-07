using Microsoft.AspNetCore.Mvc;

namespace WMS.WebUI.Controllers;

public class ErrorsController : Controller
{
    public ActionResult NotFound()
    {
        return View();
    }

    public ActionResult Forbidden()
    {
        return View();
    }

    public ActionResult Unauthorized()
    {
        return View();
    }

    public ActionResult InternalError()
    {
        return View();
    }
}
