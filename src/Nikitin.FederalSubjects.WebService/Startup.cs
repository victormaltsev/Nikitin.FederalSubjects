using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Host = Nikitin.FederalSubjects.WebAssembly.Host;

namespace Nikitin.FederalSubjects.WebService;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

        services.AddControllers();
        services.AddRazorComponents().AddInteractiveWebAssemblyComponents();

        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Nikitin.FederalSubjects.WebService", Version = "v1" });
        });

        services.AddHealthChecks();
        services.AddProblemDetails();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        if (env.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseStaticFiles();
        app.UseHsts();
        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAntiforgery();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorComponents<Host>().AddInteractiveWebAssemblyRenderMode();

            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapHealthChecks("/health/lite", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                Predicate = check => !check.Tags.Contains("deep")
            });
        });
    }
}
