using FluentAssertions;
using Nikitin.FederalSubjects.Application.Identities;
using Nikitin.FederalSubjects.Database.Models;
using Nikitin.FederalSubjects.Infrastructure.Repositories;
using Xunit;

namespace Nikitin.FederalSubjects.Infrastructure.Tests.Repositories;

public class FederalDistrictsRepositoryTests
{
    private readonly FederalDistrictsRepository _target;
    private readonly InMemoryDbContext _dbContext = new();

    public FederalDistrictsRepositoryTests()
    {
        _target = new FederalDistrictsRepository(_dbContext);
    }

    [Fact]
    public async Task GetAllDistrictsAsync_ShouldBeCorrect()
    {
        // setup
        var federalDistricts = new[]
        {
            new FederalDistrictDbModel { Name = Guid.NewGuid().ToString("N") },
            new FederalDistrictDbModel { Name = Guid.NewGuid().ToString("N") },
            new FederalDistrictDbModel { Name = Guid.NewGuid().ToString("N") }
        };

        await _dbContext.FederalDistricts.AddRangeAsync(federalDistricts);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _target.GetAllDistrictsAsync();

        // assert
        var expected = new[]
        {
            MakeExpected(federalDistricts[0]),
            MakeExpected(federalDistricts[1]),
            MakeExpected(federalDistricts[2])
        };

        result.Should().BeEquivalentTo(expected);

        static object MakeExpected(FederalDistrictDbModel federalDistrict) => new
        {
            FederalDistrictId = new FederalDistrictId(federalDistrict.Id),
            federalDistrict.Name
        };
    }
}
