using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Nikitin.FederalSubjects.Application.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace Nikitin.FederalSubjects.WebService.Controllers;

[ApiController]
[Route("federal-districts")]
[Produces(MediaTypeNames.Application.Json)]
public class FederalDistrictsController : ControllerBase
{
    private readonly IFederalDistrictsRepository _federalDistrictsRepository;

    public FederalDistrictsController(IFederalDistrictsRepository federalDistrictsRepository)
    {
        _federalDistrictsRepository = federalDistrictsRepository;
    }

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(FederalDistrictsResponse))]
    public async Task<IActionResult> GetAllDistrictsAsync()
    {
        var federalDistricts = await _federalDistrictsRepository.GetAllDistrictsAsync();
        return Ok(new FederalDistrictsResponse
        {
            Elements = federalDistricts.Select(x => new ElementResponse
            {
                FederalDistrictId = x.FederalDistrictId,
                Name = x.Name
            })
        });
    }

    private record FederalDistrictsResponse
    {
        public IEnumerable<ElementResponse> Elements { get; init; } = null!;
    }

    private record ElementResponse
    {
        public short FederalDistrictId { get; init; }
        public string Name { get; init; } = null!;
    }
}
