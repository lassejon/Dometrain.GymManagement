using GymManagement.Domain.Subscriptions;
using ErrorOr;

namespace GymManagement.Application.Common.Interfaces;

public interface ISubscriptionRepository
{
    Task AddAsync(Subscription subscription);
    Task<bool> ExistsAsync(Guid id);
    Task<Subscription?> GetByAdminIdAsync(Guid adminId);
    Task<Subscription?> GetByIdAsync(Guid id);
    Task<List<Subscription>> ListAsync();
    Task RemoveAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
}