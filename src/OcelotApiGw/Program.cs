
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle());
// Add services to the container.
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);
/*builder.WebHost.ConfigureAppConfiguration(config =>
{
    config.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

});*/

builder.WebHost.ConfigureLogging(loggingBuilder =>
{
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});



var app = builder.Build();


app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
