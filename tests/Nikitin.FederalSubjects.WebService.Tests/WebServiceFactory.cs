using Microsoft.AspNetCore.Mvc.Testing;

namespace Nikitin.FederalSubjects.WebService.Tests;

public class WebServiceFactory : WebApplicationFactory<Startup>
{
    public HttpClient HttpClient { get; }

    public WebServiceFactory()
    {
        HttpClient = CreateClient();
    }
}
