using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;
using ErrorOr;

namespace GymManagement.Application.Queries.GetSubscription;

public class GetSubscriptionCommandHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository _subscriptionsRepository;

    public GetSubscriptionCommandHandler(ISubscriptionRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery query,  CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(query.SubscriptionId);

        return subscription is null ? Error.NotFound(description: "Subscription not found") : subscription;
    }
}