var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHealthChecksUI(setup =>
    {
        setup.MaximumHistoryEntriesPerEndpoint(50);

        setup.ConfigureApiEndpointHttpclient((sp, client) =>
        {
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        setup.SetHeaderText("This is not the DOCKER image.");
    })
    .AddSqlServerStorage(builder.Configuration.GetConnectionString("Health"));

var app = builder.Build();

app.UseRouting();

app.UseStaticFiles();

app.UseEndpoints(config =>
{
    config.MapHealthChecksUI(setup =>
    {
        setup.UIPath = "/";
        setup.AddCustomStylesheet("wwwroot/dotnet.css");
    });
});

app.Run();
