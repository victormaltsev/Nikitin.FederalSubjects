using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Reflection;

namespace Nikitin.FederalSubjects.WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class VersionController : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(VersionResponse))]
    public IActionResult Version()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? throw new NullReferenceException("version cannot be null");
        return Ok(new VersionResponse { Version = version });
    }

    public record VersionResponse
    {
        public string Version { get; init; } = null!;
    }
}
