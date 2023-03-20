using FluentAssertions;
using Nikitin.FederalSubjects.Database.Models;
using Xunit;

namespace Nikitin.FederalSubjects.WebService.Tests.Endpoints;

public class FederalDistrictsEndpointsTests
{
    private readonly WebServiceFactory _factory = new();

    [Fact]
    public async Task FederalDistricts_ShouldBeCorrect()
    {
        // setup
        var federalDistricts = new[]
        {
            new FederalDistrictDbModel { Name = Guid.NewGuid().ToString("N") },
            new FederalDistrictDbModel { Name = Guid.NewGuid().ToString("N") },
            new FederalDistrictDbModel { Name = Guid.NewGuid().ToString("N") }
        };

        await _factory.DbContext.FederalDistricts.AddRangeAsync(federalDistricts);
        await _factory.DbContext.SaveChangesAsync();

        // act
        var response = await _factory.HttpClient.GetAsync("federal-districts");

        // assert
        var expected = new
        {
            elements = new[]
            {
                new
                {
                    federalDistrictId = federalDistricts[0].Id,
                    name = federalDistricts[0].Name
                },
                new
                {
                    federalDistrictId = federalDistricts[1].Id,
                    name = federalDistricts[1].Name
                },
                new
                {
                    federalDistrictId = federalDistricts[2].Id,
                    name = federalDistricts[2].Name
                }
            }
        };

        response.Should().Be200Ok().And.BeAs(expected);
    }
}
