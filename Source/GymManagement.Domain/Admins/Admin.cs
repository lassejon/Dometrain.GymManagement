using GymManagement.Domain.Subscriptions;

namespace GymManagement.Domain.Admins;

public class Admin
{
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; private set; } = null;
    public Guid Id { get; private set; }

    public Admin(
        Guid userId,
        Guid? subscriptionId = null,
        Guid? id = null)
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
        Id = id ?? Guid.NewGuid();
    }

    private Admin() { }

    public void SetSubscription(Subscription subscription)
    {
        if (SubscriptionId.HasValue)
        {
            throw new ArgumentException();
        }

        SubscriptionId = subscription.Id;
    }

    public void DeleteSubscription(Guid subscriptionId)
    {
        if (SubscriptionId is null || subscriptionId != SubscriptionId)
        {
            throw new ArgumentException();
        }

        SubscriptionId = null;
    }
}