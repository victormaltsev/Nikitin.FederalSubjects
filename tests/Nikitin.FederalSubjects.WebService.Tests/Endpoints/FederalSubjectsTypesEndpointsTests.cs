using FluentAssertions;
using Nikitin.FederalSubjects.Database.Models;
using Xunit;

namespace Nikitin.FederalSubjects.WebService.Tests.Endpoints;

public class FederalSubjectsTypesEndpointsTests
{
    private readonly WebServiceFactory _factory = new();

    [Fact]
    public async Task FederalSubjectsTypes_ShouldBeCorrect()
    {
        // setup
        var federalSubjectsTypes = new[]
        {
            new FederalSubjectTypeDbModel { Name = Guid.NewGuid().ToString("N") },
            new FederalSubjectTypeDbModel { Name = Guid.NewGuid().ToString("N") },
            new FederalSubjectTypeDbModel { Name = Guid.NewGuid().ToString("N") }
        };

        await _factory.DbContext.FederalSubjectsTypes.AddRangeAsync(federalSubjectsTypes);
        await _factory.DbContext.SaveChangesAsync();

        // act
        var response = await _factory.HttpClient.GetAsync("federal-subjects-types");

        // assert
        var expected = new
        {
            elements = new[]
            {
                new
                {
                    federalSubjectTypeId = federalSubjectsTypes[0].Id,
                    name = federalSubjectsTypes[0].Name
                },
                new
                {
                    federalSubjectTypeId = federalSubjectsTypes[1].Id,
                    name = federalSubjectsTypes[1].Name
                },
                new
                {
                    federalSubjectTypeId = federalSubjectsTypes[2].Id,
                    name = federalSubjectsTypes[2].Name
                }
            }
        };

        response.Should().Be200Ok().And.BeAs(expected);
    }
}
