using System.Net.Mime;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Nikitin.FederalSubjects.WebService.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class VersionController : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(VersionResponse))]
    public IActionResult GetVersion() =>
        Ok(new VersionResponse { Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() });

    private record VersionResponse
    {
        public string? Version { get; init; }
    }
}
