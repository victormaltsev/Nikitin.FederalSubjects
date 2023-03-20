using Nikitin.FederalSubjects.Application.Identities;

namespace Nikitin.FederalSubjects.Application.Repositories;

public interface IFederalSubjectsTypesRepository
{
    Task<IReadOnlyList<FederalSubjectType>> GetAllSubjectsTypesAsync();
}

public record FederalSubjectType
{
    public FederalSubjectTypeId FederalSubjectTypeId { get; init; }
    public string Name { get; init; } = null!;
}
