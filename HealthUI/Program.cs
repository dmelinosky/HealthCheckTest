var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHealthChecksUI(setup =>
    {
        setup.MaximumHistoryEntriesPerEndpoint(50);

        setup.SetEvaluationTimeInSeconds(120);

        setup.ConfigureApiEndpointHttpclient((sp, client) =>
        {
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        setup.SetHeaderText("This is not the DOCKER image.");

        setup.AddWebhookNotification(
            "Fake Email Service",
            uri: "http://api3/api/webhookreport/outage",
            payload: "{ \"message\": \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
            restorePayload: "{ \"message\": \"[[LIVENESS]] is back to life\"}"
            );
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
