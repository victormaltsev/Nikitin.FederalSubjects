using Microsoft.EntityFrameworkCore;
using Nikitin.FederalSubjects.Application.Identities;
using Nikitin.FederalSubjects.Application.Repositories;
using Nikitin.FederalSubjects.Database;

namespace Nikitin.FederalSubjects.Infrastructure.Repositories;

public class FederalSubjectsTypesRepository : IFederalSubjectsTypesRepository
{
    private readonly AppDbContext _dbContext;

    public FederalSubjectsTypesRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<FederalSubjectType>> GetAllSubjectsTypesAsync() =>
        await _dbContext.FederalSubjectsTypes.Select(x => new FederalSubjectType
        {
            FederalSubjectTypeId = new FederalSubjectTypeId(x.Id),
            Name = x.Name
        }).ToListAsync();
}
