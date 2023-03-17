using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Nikitin.FederalSubjects.Application.Repositories;
using Nikitin.FederalSubjects.Database;
using Nikitin.FederalSubjects.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

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

        services.AddScoped<IFederalDistrictsRepository, FederalDistrictsRepository>();
        services.AddScoped<IFederalSubjectsRepository, FederalSubjectsRepository>();
        services.AddScoped<IFederalSubjectsTypesRepository, FederalSubjectsTypesRepository>();

        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        });

        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerGen(SwaggerGenOptions);

        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>(
                name: "PostgreSQL",
                tags: new[] { "deep" }
            );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(SwaggerUIOptions);
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

    private static void SwaggerGenOptions(SwaggerGenOptions options)
    {
        options.EnableAnnotations();
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Nikitin.FederalSubjects.WebService", Version = "v1" });
        options.CustomSchemaIds(x => x.GUID.ToString());
    }

    private static void SwaggerUIOptions(SwaggerUIOptions options)
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Nikitin.FederalSubjects.WebService v1");
        options.DefaultModelsExpandDepth(-1);
    }
}
