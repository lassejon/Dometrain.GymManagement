using GymManagement.Application.Queries.GetSubscription;
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionRequest request)
    {
        var command = new CreateSubscriptionCommand(request.SubscriptionType.ToString(), request.AdminId);
        var subscriptionResult= await _mediator.Send(command);

        return subscriptionResult.MatchFirst(
            value => Created("Ok", new SubscriptionResponse(value.Id, request.SubscriptionType)), 
            error => Problem());
    }

    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var query = new GetSubscriptionQuery(subscriptionId);
        var subscriptionResult = await _mediator.Send(query);

        return subscriptionResult.MatchFirst(
            value => Ok(new SubscriptionResponse(
                value.Id,
                Enum.Parse<SubscriptionType>(value.SubscriptionType ?? "Free"))),
            error => Problem());
    }
}