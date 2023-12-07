using GymManagement.Domain.Gyms;

namespace GymManagement.Application.Common.Interfaces;

public interface IGymsRepository
{
    Task AddAsync(Gym gym);
    Task<Gym?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId);
    Task UpdateAsync(Gym gym);
    Task RemoveAsync(Gym gym);
    Task RemoveRangeAsync(List<Gym> gyms);
}