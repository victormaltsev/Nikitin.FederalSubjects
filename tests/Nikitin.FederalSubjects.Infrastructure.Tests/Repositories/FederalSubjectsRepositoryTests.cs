using FluentAssertions;
using Nikitin.FederalSubjects.Application.Identities;
using Nikitin.FederalSubjects.Database.Models;
using Nikitin.FederalSubjects.Infrastructure.Repositories;
using Xunit;

namespace Nikitin.FederalSubjects.Infrastructure.Tests.Repositories;

public class FederalSubjectsRepositoryTests
{
    private readonly FederalSubjectsRepository _target;
    private readonly InMemoryDbContext _dbContext = new();

    public FederalSubjectsRepositoryTests()
    {
        _target = new FederalSubjectsRepository(_dbContext);
    }

    [Fact]
    public async Task GetAllSubjectsAsync_ShouldBeCorrect()
    {
        // setup
        var federalSubjects = new[]
        {
            MakeRandomFederalSubject(),
            MakeRandomFederalSubject(),
            MakeRandomFederalSubject()
        };

        await _dbContext.FederalSubjects.AddRangeAsync(federalSubjects);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _target.GetAllSubjectsAsync();

        // assert
        var expected = new[]
        {
            MakeExpected(federalSubjects[0]),
            MakeExpected(federalSubjects[1]),
            MakeExpected(federalSubjects[2])
        };

        result.Should().BeEquivalentTo(expected);

        static FederalSubjectDbModel MakeRandomFederalSubject() => new()
        {
            Name = Guid.NewGuid().ToString("N"),
            Description = Guid.NewGuid().ToString("N"),
            FederalDistrict = new FederalDistrictDbModel(),
            FederalSubjectType = new FederalSubjectTypeDbModel()
        };

        static object MakeExpected(FederalSubjectDbModel federalSubject) => new
        {
            FederalSubjectId = new FederalSubjectId(federalSubject.Id),
            FederalDistrictId = new FederalDistrictId(federalSubject.FederalDistrictId),
            FederalSubjectTypeId = new FederalSubjectTypeId(federalSubject.FederalSubjectTypeId),
            federalSubject.Name,
            federalSubject.Description
        };
    }

    [Theory]
    [InlineData(100)]
    [InlineData(101)]
    [InlineData(102)]
    public async Task GetContentAsync_ShouldBeCorrect(short expected)
    {
        // setup
        var federalSubjects = new[]
        {
            new FederalSubjectDbModel { Id = 100, Content = Guid.NewGuid().ToString("N") },
            new FederalSubjectDbModel { Id = 101, Content = Guid.NewGuid().ToString("N") },
            new FederalSubjectDbModel { Id = 102, Content = Guid.NewGuid().ToString("N") }
        };

        await _dbContext.FederalSubjects.AddRangeAsync(federalSubjects);
        await _dbContext.SaveChangesAsync();

        var federalSubjectId = new FederalSubjectId(expected);

        // act
        var result = await _target.GetContentAsync(federalSubjectId);

        // assert
        result.Should().Be(federalSubjects.Single(x => x.Id == expected).Content);
    }
}
