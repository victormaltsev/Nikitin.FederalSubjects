using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Nikitin.FederalSubjects.Application.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace Nikitin.FederalSubjects.WebService.Controllers;

[ApiController]
[Route("federal-subjects-types")]
[Produces(MediaTypeNames.Application.Json)]
public class FederalSubjectsTypesController : ControllerBase
{
    private readonly IFederalSubjectsTypesRepository _federalSubjectsTypesRepository;

    public FederalSubjectsTypesController(IFederalSubjectsTypesRepository federalSubjectsTypesRepository)
    {
        _federalSubjectsTypesRepository = federalSubjectsTypesRepository;
    }

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(FederalSubjectsTypesResponse))]
    public async Task<IActionResult> GetAllSubjectsTypesAsync()
    {
        var federalSubjectsTypes = await _federalSubjectsTypesRepository.GetAllSubjectsTypesAsync();
        return Ok(new FederalSubjectsTypesResponse
        {
            Elements = federalSubjectsTypes.Select(x => new ElementResponse
            {
                FederalSubjectTypeId = x.FederalSubjectTypeId,
                Name = x.Name
            })
        });
    }

    private record FederalSubjectsTypesResponse
    {
        public IEnumerable<ElementResponse> Elements { get; init; } = null!;
    }

    private record ElementResponse
    {
        public short FederalSubjectTypeId { get; init; }
        public string Name { get; init; } = null!;
    }
}
