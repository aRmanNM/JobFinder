using JobFinder.Server.Helpers;
using JobFinder.Server.Models;
using JobFinder.Server.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SubscriptionController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly CurrentUserHelper _currentUserHelper;

    public SubscriptionController(
        ApplicationDbContext db,
        CurrentUserHelper currentUserHelper)
    {
        _db = db;
        _currentUserHelper = currentUserHelper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(SubscriptionModel subscriptionModel, CancellationToken cancellationToken)
    {
        var subscription = new Subscription
        {
            UserId = _currentUserHelper.UserId,
            CreatedAt = DateTimeOffset.Now,
            SearchCount = subscriptionModel.SearchCount,
            Status = SubscriptionStatus.Unpaid,
        };

        await _db.Subscriptions.AddAsync(subscription, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return Ok(SubscriptionModel.MapFromSubscription(subscription));
    }

    [HttpGet]
    public async Task<IActionResult> GetUserSubscription(CancellationToken cancellationToken)
    {
        var userId = _currentUserHelper.UserId;

        var subscriptions = await _db.Subscriptions
            .AsNoTracking()
            .Where(s => s.UserId == userId)
            .ToListAsync(cancellationToken);

        return Ok(subscriptions
            .Select(s => SubscriptionModel.MapFromSubscription(s)));
    }
}