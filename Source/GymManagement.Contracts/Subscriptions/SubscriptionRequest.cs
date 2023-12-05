namespace GymManagement.Contracts.Subscriptions;

public record SubscriptionRequest(SubscriptionType SubscriptionType, Guid AdminId);