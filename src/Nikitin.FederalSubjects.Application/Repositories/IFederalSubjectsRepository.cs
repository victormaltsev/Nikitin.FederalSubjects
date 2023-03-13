using Nikitin.FederalSubjects.Application.Identities;

namespace Nikitin.FederalSubjects.Application.Repositories;

public interface IFederalSubjectsRepository
{
    Task<IReadOnlyList<FederalSubject>> GetAllSubjectsAsync();
    Task<string> GetContentAsync(FederalSubjectId federalSubjectId);
}

public record FederalSubject
{
    public FederalSubjectId FederalSubjectId { get; init; }
    public FederalDistrictId FederalDistrictId { get; init; }
    public FederalSubjectTypeId FederalSubjectTypeId { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
}
