using GymManagement.Application.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSettings<T>(this IServiceCollection services) where T : Settings<T>
    {
        var settings = Activator.CreateInstance<T>();
        
        services = settings.OnConfigure(services);

        return services;
    }
}