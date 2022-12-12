using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Nikitin.FederalSubjects.WebApplication.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
[Produces(MediaTypeNames.Text.Html)]
public class HomeController : Controller
{
    [HttpGet("/", Order = 1)]
    [HttpGet("/index", Order = 2)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/not-implemented")]
    public IActionResult NotImplemented()
    {
        throw new NotImplementedException();
    }
}
