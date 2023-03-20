using Microsoft.EntityFrameworkCore;
using Nikitin.FederalSubjects.Application.Identities;
using Nikitin.FederalSubjects.Application.Repositories;
using Nikitin.FederalSubjects.Database;

namespace Nikitin.FederalSubjects.Infrastructure.Repositories;

public class FederalSubjectsRepository : IFederalSubjectsRepository
{
    private readonly AppDbContext _dbContext;

    public FederalSubjectsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<FederalSubject>> GetAllSubjectsAsync() =>
        await _dbContext.FederalSubjects.Select(x => new FederalSubject
        {
            FederalSubjectId = new FederalSubjectId(x.Id),
            FederalDistrictId = new FederalDistrictId(x.FederalDistrictId),
            FederalSubjectTypeId = new FederalSubjectTypeId(x.FederalSubjectTypeId),
            Name = x.Name,
            Description = x.Description
        }).ToListAsync();

    public async Task<string?> GetContentAsync(FederalSubjectId federalSubjectId) =>
        await _dbContext.FederalSubjects.Where(x => x.Id == federalSubjectId)
            .Select(x => x.Content)
            .SingleAsync();
}
