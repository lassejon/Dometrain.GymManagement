using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Common.Persistence;
using GymManagement.Infrastructure.Subscriptions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<GymManagementDbContext>());
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        services.AddDbContext<GymManagementDbContext>(options =>
        {
            options.UseSqlServer("Server=.;Database=GymManagement;Trusted_Connection=True;TrustServerCertificate=true;");
        });
        
        return services;
    }
}