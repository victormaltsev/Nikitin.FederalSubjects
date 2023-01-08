using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Nikitin.FederalSubjects.WebApplication.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
[Produces(MediaTypeNames.Text.Html)]
public class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/index")]
    public IActionResult IndexRedirect()
    {
        return RedirectToAction("Index");
    }

    [HttpGet("/map")]
    public IActionResult Map()
    {
        return View();
    }

    [HttpGet("/not-implemented")]
    public IActionResult NotImplemented()
    {
        throw new NotImplementedException();
    }
}
