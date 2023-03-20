using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Nikitin.FederalSubjects.Application.Identities;
using Nikitin.FederalSubjects.Application.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace Nikitin.FederalSubjects.WebService.Controllers;

[ApiController]
[Route("federal-subjects")]
public class FederalSubjectsController : ControllerBase
{
    private readonly IFederalSubjectsRepository _federalSubjectsRepository;

    public FederalSubjectsController(IFederalSubjectsRepository federalSubjectsRepository)
    {
        _federalSubjectsRepository = federalSubjectsRepository;
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(FederalSubjectsResponse))]
    public async Task<IActionResult> GetAllSubjectsAsync()
    {
        var federalSubjects = await _federalSubjectsRepository.GetAllSubjectsAsync();
        return Ok(new FederalSubjectsResponse
        {
            Elements = federalSubjects.Select(x => new ElementResponse
            {
                FederalSubjectId = x.FederalSubjectId,
                FederalDistrictId = x.FederalDistrictId,
                FederalSubjectTypeId = x.FederalSubjectTypeId,
                Name = x.Name,
                Description = x.Description
            })
        });
    }

    [HttpGet("content/{id:int}")]
    [Produces(MediaTypeNames.Text.Html)]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<IActionResult> GetContentAsync([FromRoute] short id)
    {
        var federalSubjectId = new FederalSubjectId(id);
        return new ContentResult
        {
            StatusCode = StatusCodes.Status200OK,
            Content = await _federalSubjectsRepository.GetContentAsync(federalSubjectId),
            ContentType = MediaTypeNames.Text.Html
        };
    }

    private record FederalSubjectsResponse
    {
        public IEnumerable<ElementResponse> Elements { get; init; } = null!;
    }

    private record ElementResponse
    {
        public short FederalSubjectId { get; init; }
        public short FederalDistrictId { get; init; }
        public short FederalSubjectTypeId { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
    }
}
