using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Nikitin.FederalSubjects.WebService.Tests.Endpoints;

public class DefaultEndpointsTests : IClassFixture<WebServiceFactory>
{
    private readonly WebServiceFactory _factory;

    public DefaultEndpointsTests(WebServiceFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/health")]
    [InlineData("/health/lite")]
    public async Task HealthChecks_ShouldBeCorrect(string url)
    {
        // setup
        // nothing

        // act
        var response = await _factory.HttpClient.GetAsync(url);

        // assert
        response.Should().Be200Ok();
    }

    [Fact]
    public async Task Version_ShouldBeCorrect()
    {
        // setup
        // nothing

        // act
        var response = await _factory.HttpClient.GetAsync("/version");

        // assert
        response.Should().Be200Ok();

        var content = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(), new { Version = null as string });
        content!.Version.Should().Match("*.*.*.*");
    }
}
