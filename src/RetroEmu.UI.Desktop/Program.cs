using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetroEmu.UI.Desktop;
using RetroEmu.UI.Desktop.Setup;

var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(config)
    .AddLogging()
    .AddDesktopApplication()
    .BuildServiceProvider();

var game = serviceProvider.GetRequiredService<IGame>();
game.Run();