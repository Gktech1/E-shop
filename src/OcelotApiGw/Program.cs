
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot();

builder.WebHost.ConfigureLogging(loggingBuilder =>
{
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});



var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.UseOcelot();

app.Run();
