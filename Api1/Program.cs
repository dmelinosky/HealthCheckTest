var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHealthChecks()
    ////.AddTypeActivatedCheck<WeirdHealthCheck>(
    ////    "Service 1",
    ////    failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
    ////    tags: new[] { "service1" },
    ////    args: new object[] { 0, 20, "Service one available", "Service one unavailable" })
    ////.AddTypeActivatedCheck<WeirdHealthCheck>(
    ////    "Service 2",
    ////    failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded,
    ////    tags: new[] { "service2" },
    ////    args: new object[] { 40, 59, "Service two available", "Service two unavailable" })
    .AddSqlServer(
        builder.Configuration.GetConnectionString("db"),
        name: "DockerUI db",
        failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
        tags: new[] { "DockerUI", "database" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(configure =>
{
    configure.MapRazorPages();
    configure.MapHealthChecks("health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.Run();
