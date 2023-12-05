using GymManagement.Application.Queries.GetSubscription;
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;

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
    public async Task<IActionResult> CreateSubscription([FromBody] SubscriptionRequest request)
    {
        if (!Enum.TryParse(typeof(DomainSubscriptionType), 
                request.SubscriptionType.ToString(), 
                out var subscriptionType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid subscription type",
                detail: $"Could not convert type {typeof(SubscriptionType)} to {typeof(Domain.Subscriptions.SubscriptionType)}. Value: {request.SubscriptionType}");
        }
        
        var command = new CreateSubscriptionCommand((DomainSubscriptionType) subscriptionType, request.AdminId);
        var subscriptionResult= await _mediator.Send(command);

        return subscriptionResult.MatchFirst(
            value => Created("Ok", new SubscriptionResponse(value.Id, request.SubscriptionType)), 
            error => Problem(detail: error.Description));
    }

    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var query = new GetSubscriptionQuery(subscriptionId);
        var subscriptionResult = await _mediator.Send(query);

        return subscriptionResult.MatchFirst(
            value => Ok(new SubscriptionResponse(
                value.Id,
                Enum.Parse<SubscriptionType>(value.SubscriptionType.ToString() ?? "Free"))),
            error => Problem());
    }
}