using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GymManagement.Application.Settings;

namespace GymManagement.Infrastructure.Settings;

public class DatabaseSettings : Settings<DatabaseSettings>
{
    private string ConnectionString { get; set; } = default!;

    public override IServiceCollection OnConfigure(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services = base.OnConfigure(services);
        
        ConnectionString = configuration[$"{SectionName}:{nameof(ConnectionString)}"]!;
        
        services.AddDbContext<GymManagementDbContext>(options =>
            options.UseSqlServer(ConnectionString));

        return services;
    }
}