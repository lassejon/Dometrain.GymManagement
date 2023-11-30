using GymManagement.Domain.Subscriptions;
using ErrorOr;

namespace GymManagement.Application.Common.Interfaces;

public interface ISubscriptionRepository
{
    Task AddAsync(Subscription subscription);
    Task<Subscription?> GetByIdAsync(Guid subscriptionId);
}