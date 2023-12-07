using ErrorOr;
using GymManagement.Domain.Gyms;

namespace GymManagement.Domain.Subscriptions;

public class Subscription
{
    private readonly List<Guid> _gymIds = new();
    private readonly int _maxGyms;

    public Guid Id { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; }

    public Guid AdminId { get; }

    private Subscription() { }

    public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null)
    {
        SubscriptionType = subscriptionType;
        AdminId = adminId;
        Id = id ?? Guid.NewGuid();
        
        _maxGyms = GetMaxGyms();
    }
    
    
    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Contains(gym.Id))
        {
            throw new InvalidOperationException();
        }

        if (_gymIds.Count >= _maxGyms)
        {
            return SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows;
        }

        _gymIds.Add(gym.Id);

        return Result.Success;
    }

    public int GetMaxGyms() => SubscriptionType switch
    {
        SubscriptionType.Free => 1,
        SubscriptionType.Starter => 1,
        SubscriptionType.Pro => 3,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxRooms() => SubscriptionType switch
    {
        SubscriptionType.Free => 1,
        SubscriptionType.Starter => 3,
        SubscriptionType.Pro => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => SubscriptionType switch
    {
        SubscriptionType.Free => 4,
        SubscriptionType.Starter => int.MaxValue,
        SubscriptionType.Pro => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public bool HasGym(Guid gymId)
    {
        return _gymIds.Contains(gymId);
    }

    public void RemoveGym(Guid gymId)
    {
        if (!_gymIds.Contains(gymId))
        {
            throw new InvalidOperationException();
        }

        _gymIds.Remove(gymId);
    }
}