using Nikitin.FederalSubjects.Application.Identities;

namespace Nikitin.FederalSubjects.Application.Repositories;

public interface IFederalDistrictsRepository
{
    Task<IReadOnlyList<FederalDistrict>> GetAllDistrictsAsync();
}

public record FederalDistrict
{
    public FederalDistrictId FederalDistrictId { get; init; }
    public string Name { get; init; } = null!;
}
