using Microsoft.EntityFrameworkCore;
using Nikitin.FederalSubjects.Application.Identities;
using Nikitin.FederalSubjects.Application.Repositories;
using Nikitin.FederalSubjects.Database;

namespace Nikitin.FederalSubjects.Infrastructure.Repositories;

public class FederalDistrictsRepository : IFederalDistrictsRepository
{
    private readonly AppDbContext _dbContext;

    public FederalDistrictsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<FederalDistrict>> GetAllDistrictsAsync() =>
        await _dbContext.FederalDistricts.Select(x => new FederalDistrict
        {
            FederalDistrictId = new FederalDistrictId(x.Id),
            Name = x.Name
        }).ToListAsync();
}
