using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HardwareStore.HealthCheckApi.HealthChecks;
using HardwareStore.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<HardwareStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HardwareStoreDb"));
});

builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "api" })
    //.AddDbContextCheck<HardwareStoreDbContext>(tags: new[] { "database" })
    .AddTypeActivatedCheck<PingHealthCheck>("Firebase", HealthStatus.Degraded, tags: new[] { "api" },
        args: "firebase.com")
    .AddTypeActivatedCheck<PingHealthCheck>("Azure", HealthStatus.Unhealthy, tags: new[] { "api" }, args: "azure.com")
    .AddSqlServer(builder.Configuration.GetConnectionString("HardwareStoreDb")!, name: "BD 01", failureStatus: HealthStatus.Unhealthy, tags: new[] { "database" })
    .AddSqlServer(builder.Configuration.GetConnectionString("Bd2")!, name: "BD 02", failureStatus: HealthStatus.Unhealthy, tags: new[] { "database" });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = x => x.Tags.Contains("api")
});

app.MapHealthChecks("/health/databases", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    Predicate = x => x.Tags.Contains("database")
});

app.MapHealthChecksUI();

app.Run();
