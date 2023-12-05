namespace GymManagement.Domain.Subscriptions;

public class Subscription
{
    private readonly Guid _adminId;
    public Guid Id { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; }

    private Subscription() { }

    public Subscription(SubscriptionType subscriptionType, Guid admindId, Guid? id = null)
    {
        SubscriptionType = subscriptionType;
        _adminId = admindId;
        Id = id ?? Guid.NewGuid();
    }
}