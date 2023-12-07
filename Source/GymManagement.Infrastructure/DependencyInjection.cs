using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Admins.Persistence;
using GymManagement.Infrastructure.Common.Persistence;
using GymManagement.Infrastructure.Gyms.Persistence;
using GymManagement.Infrastructure.Settings;
using GymManagement.Infrastructure.Subscriptions.Persistence;
using GymManagement.Application.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.ConfigureSettings<DatabaseSettings>();

        services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<GymManagementDbContext>());
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IGymsRepository, GymsRepository>();
        
        return services;
    }
}