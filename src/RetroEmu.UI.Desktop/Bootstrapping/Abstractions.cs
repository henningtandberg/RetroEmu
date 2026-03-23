using Microsoft.Extensions.DependencyInjection;

namespace RetroEmu.UI.Desktop.Bootstrapping;

public static class Abstractions
{
    public static IServiceCollection AddAbstraction<TAbstraction, TInterface, TImplementation>(
        this IServiceCollection serviceCollection) where TImplementation: TInterface, TAbstraction where TAbstraction : class
    {
        return serviceCollection.AddSingleton<TAbstraction>(serviceProvider =>
            (TImplementation)serviceProvider.GetRequiredService<TInterface>());
    }
}