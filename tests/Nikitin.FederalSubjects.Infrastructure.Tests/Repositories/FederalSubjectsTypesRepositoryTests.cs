using FluentAssertions;
using Nikitin.FederalSubjects.Application.Identities;
using Nikitin.FederalSubjects.Database.Models;
using Nikitin.FederalSubjects.Infrastructure.Repositories;
using Xunit;

namespace Nikitin.FederalSubjects.Infrastructure.Tests.Repositories;

public class FederalSubjectsTypesRepositoryTests
{
    private readonly FederalSubjectsTypesRepository _target;
    private readonly InMemoryDbContext _dbContext = new();

    public FederalSubjectsTypesRepositoryTests()
    {
        _target = new FederalSubjectsTypesRepository(_dbContext);
    }

    [Fact]
    public async Task GetAllSubjectsTypesAsync_ShouldBeCorrect()
    {
        // setup
        var federalSubjectsTypes = new[]
        {
            new FederalSubjectTypeDbModel { Name = Guid.NewGuid().ToString("N") },
            new FederalSubjectTypeDbModel { Name = Guid.NewGuid().ToString("N") },
            new FederalSubjectTypeDbModel { Name = Guid.NewGuid().ToString("N") }
        };

        await _dbContext.FederalSubjectsTypes.AddRangeAsync(federalSubjectsTypes);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _target.GetAllSubjectsTypesAsync();

        // assert
        var expected = new[]
        {
            MakeExpected(federalSubjectsTypes[0]),
            MakeExpected(federalSubjectsTypes[1]),
            MakeExpected(federalSubjectsTypes[2])
        };

        result.Should().BeEquivalentTo(expected);

        static object MakeExpected(FederalSubjectTypeDbModel federalSubjectType) => new
        {
            FederalSubjectTypeId = new FederalSubjectTypeId(federalSubjectType.Id),
            federalSubjectType.Name
        };
    }
}
