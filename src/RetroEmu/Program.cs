using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetroEmu;

var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(config)
    .AddLogging()
    .AddGame()
    .BuildServiceProvider();

var game = serviceProvider.GetRequiredService<IGame>();
game.Run();