using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace WMS.WebUI.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is HttpRequestException apiException)
        {
            var code = GetStatusCode(apiException.StatusCode);
            context.Result = new RedirectToActionResult("Error", "Home", new { statusCode = code });
            context.ExceptionHandled = true;
        }
        else
        {
            context.Result = new RedirectToActionResult("Error", "Home", new { statusCode = 500 });
            context.ExceptionHandled = true;
        }
    }

    private static int GetStatusCode(HttpStatusCode? code) =>
        code switch
        {
            HttpStatusCode.NotFound => 404,
            _ => 500
        };
}
