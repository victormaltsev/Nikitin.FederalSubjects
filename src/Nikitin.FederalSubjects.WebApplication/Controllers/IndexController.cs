using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Nikitin.FederalSubjects.WebApplication.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
[Produces(MediaTypeNames.Text.Html)]
public class IndexController : Controller
{
    [HttpGet("/")]
    [HttpGet("/index")]
    public IActionResult Index()
    {
        return View();
    }
}
