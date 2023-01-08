using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RetroEmu;

public static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => services
                    .AddHostedService<Worker>()
                    .AddSingleton<IGameInstance, GameInstance>());
}