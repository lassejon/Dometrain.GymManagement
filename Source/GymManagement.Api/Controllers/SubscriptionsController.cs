using GymManagement.Application.Queries.GetSubscription;
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Subscriptions.Commands.DeleteSubscription;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISender _mediator;

    public SubscriptionsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(SubscriptionRequest request)
    {
        if (!Enum.TryParse(typeof(DomainSubscriptionType), 
                request.SubscriptionType.ToString(), 
                out var subscriptionType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid subscription type");
        }

        var command = new CreateSubscriptionCommand(
            (DomainSubscriptionType) subscriptionType,
            request.AdminId);

        var createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.MatchFirst(
            subscription => CreatedAtAction(
                nameof(GetSubscription),
                new { subscriptionId = subscription.Id },
                new SubscriptionResponse(
                    subscription.Id,
                    ToDto(subscription.SubscriptionType))),
            error => Problem());
    }

    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var query = new GetSubscriptionQuery(subscriptionId);

        var getSubscriptionsResult = await _mediator.Send(query);

        return getSubscriptionsResult.MatchFirst(
            subscription => Ok(new SubscriptionResponse(
                subscription.Id,
                ToDto(subscription.SubscriptionType))),
            error => Problem());
    }

    [HttpDelete("{subscriptionId:guid}")]
    public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
    {
        var command = new DeleteSubscriptionCommand(subscriptionId);

        var createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.Match<IActionResult>(
            _ => NoContent(),
            _ => Problem());
    }

    private static SubscriptionType ToDto(DomainSubscriptionType subscriptionType)
    {
        return subscriptionType switch
        {
            DomainSubscriptionType.Free => SubscriptionType.Free,
            DomainSubscriptionType.Starter => SubscriptionType.Starter,
            DomainSubscriptionType.Pro => SubscriptionType.Pro,
            _ => throw new InvalidOperationException(),
        };
    }
}