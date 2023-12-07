using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.ListGyms;

public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public ListGymsQueryHandler(IGymsRepository gymsRepository, ISubscriptionRepository subscriptionRepository)
    {
        _gymsRepository = gymsRepository;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery query, CancellationToken cancellationToken)
    {
        if (!await _subscriptionRepository.ExistsAsync(query.SubscriptionId))
        {
            return Error.NotFound(description: "Subscription not found");
        }

        return await _gymsRepository.ListBySubscriptionIdAsync(query.SubscriptionId);
    }
}
