using FluentAssertions;
using Nikitin.FederalSubjects.Database.Models;
using Xunit;

namespace Nikitin.FederalSubjects.WebService.Tests.Endpoints;

public class FederalSubjectsEndpointsTests
{
    private readonly WebServiceFactory _factory = new();

    [Fact]
    public async Task FederalSubjects_ShouldBeCorrect()
    {
        // setup
        var federalSubjects = new[]
        {
            MakeRandomFederalSubject(),
            MakeRandomFederalSubject(),
            MakeRandomFederalSubject()
        };

        await _factory.DbContext.FederalSubjects.AddRangeAsync(federalSubjects);
        await _factory.DbContext.SaveChangesAsync();

        // act
        var response = await _factory.HttpClient.GetAsync("federal-subjects");

        // assert
        var expected = new
        {
            elements = new[]
            {
                new
                {
                    federalSubjectId = federalSubjects[0].Id,
                    federalDistrictId = federalSubjects[0].FederalDistrictId,
                    federalSubjectTypeId = federalSubjects[0].FederalSubjectTypeId,
                    name = federalSubjects[0].Name,
                    description = federalSubjects[0].Description
                },
                new
                {
                    federalSubjectId = federalSubjects[1].Id,
                    federalDistrictId = federalSubjects[1].FederalDistrictId,
                    federalSubjectTypeId = federalSubjects[1].FederalSubjectTypeId,
                    name = federalSubjects[1].Name,
                    description = federalSubjects[1].Description
                },
                new
                {
                    federalSubjectId = federalSubjects[2].Id,
                    federalDistrictId = federalSubjects[2].FederalDistrictId,
                    federalSubjectTypeId = federalSubjects[2].FederalSubjectTypeId,
                    name = federalSubjects[2].Name,
                    description = federalSubjects[2].Description
                }
            }
        };

        response.Should().Be200Ok().And.BeAs(expected);

        static FederalSubjectDbModel MakeRandomFederalSubject() => new()
        {
            Name = Guid.NewGuid().ToString("N"),
            Description = Guid.NewGuid().ToString("N"),
            FederalDistrict = new FederalDistrictDbModel(),
            FederalSubjectType = new FederalSubjectTypeDbModel()
        };
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task FederalSubjects_Content_ShouldBeCorrect(int index)
    {
        // setup
        var federalSubjects = new[]
        {
            new FederalSubjectDbModel { Content = Guid.NewGuid().ToString("N") },
            new FederalSubjectDbModel { Content = Guid.NewGuid().ToString("N") },
            new FederalSubjectDbModel { Content = Guid.NewGuid().ToString("N") }
        };

        await _factory.DbContext.FederalSubjects.AddRangeAsync(federalSubjects);
        await _factory.DbContext.SaveChangesAsync();

        // act
        var response = await _factory.HttpClient.GetAsync($"federal-subjects/content/{federalSubjects[index].Id}");

        // assert
        response.Should().Be200Ok();

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be(federalSubjects[index].Content);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task FederalSubjects_Content_WhenNullOrEmpty_ShouldBeCorrect(string? value)
    {
        // setup
        var federalSubject = new FederalSubjectDbModel { Content = value };

        await _factory.DbContext.FederalSubjects.AddAsync(federalSubject);
        await _factory.DbContext.SaveChangesAsync();

        // act
        var response = await _factory.HttpClient.GetAsync($"federal-subjects/content/{federalSubject.Id}");

        // assert
        response.Should().Be200Ok();

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be(value ?? string.Empty);
    }
}
