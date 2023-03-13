using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace Nikitin.FederalSubjects.WebService;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

        services.AddControllers().AddNewtonsoftJson();

        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerGen(x =>
        {
            x.EnableAnnotations();
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Nikitin.FederalSubjects.WebService", Version = "v1" });
        });

        services.AddHealthChecks();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Nikitin.FederalSubjects.WebService v1"));
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapHealthChecks("/health/lite", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                Predicate = (check) => !check.Tags.Contains("deep")
            });
        });
    }
}
