using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RetroEmu.Cli;
using RetroEmu.Devices.DMG;

var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IApplication, Application>();
        services.AddDotMatrixGameBoy();
    })
    .Build();

using var scope = host.Services.CreateScope();

try
{
    scope
        .ServiceProvider
        .GetRequiredService<IApplication>()
        .Run(args);
}
catch (Exception e)
{
    Console.Error.WriteLine(e.Message);
}