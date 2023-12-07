using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly GymManagementDbContext _dbContext;

    public SubscriptionRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Subscription subscription)
    {
        await _dbContext.Subscriptions.AddAsync(subscription);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Subscriptions
            .AsNoTracking()
            .AnyAsync(subscription => subscription.Id == id);
    }

    public async Task<Subscription?> GetByAdminIdAsync(Guid adminId)
    {
        return await _dbContext.Subscriptions
            .AsNoTracking()
            .FirstOrDefaultAsync(subscription => subscription.AdminId == adminId);
    }

    public async Task<Subscription?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Subscriptions.FirstOrDefaultAsync(subscription => subscription.Id == id);
    }

    public async Task<List<Subscription>> ListAsync()
    {
        return await _dbContext.Subscriptions.ToListAsync();
    }

    public Task RemoveAsync(Subscription subscription)
    {
        _dbContext.Remove(subscription);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Subscription subscription)
    {
        _dbContext.Update(subscription);

        return Task.CompletedTask;
    }
}