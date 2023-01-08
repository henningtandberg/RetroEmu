using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace RetroEmu;

public class Worker : IHostedService
{
    private readonly IGameInstance _gameInstance;
    private readonly IHostApplicationLifetime _appLifetime;

    public Worker(IGameInstance gameInstance, IHostApplicationLifetime appLifetime)
    {
        _gameInstance = gameInstance;
        _appLifetime = appLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(OnStarted);
        _appLifetime.ApplicationStopping.Register(OnStopping);
        _appLifetime.ApplicationStopped.Register(OnStopped);

        _gameInstance.Exiting += OnGameInstanceExiting;

        return Task.CompletedTask;
    }

    private void OnGameInstanceExiting(object sender, System.EventArgs e)
    {
        StopAsync(new CancellationToken());
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _appLifetime.StopApplication();

        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        _gameInstance.Run();
    }

    private void OnStopping()
    {
    }

    private void OnStopped()
    {
    }
}