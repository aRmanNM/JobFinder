using System.ComponentModel;
using JobFinder.Server.Helpers;
using JobFinder.Server.Models;
using JobFinder.Server.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Server.Controllers;

// a demo thing. real logic is different and requires views
//

[ApiController]
[Route("[controller]")]
public class PaymentController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly CurrentUserHelper _currentUserHelper;

    public PaymentController(
        ApplicationDbContext db,
        CurrentUserHelper currentUserHelper)
    {
        _db = db;
        _currentUserHelper = currentUserHelper;
    }

    [HttpPost("Pay")]
    [Authorize]
    public async Task<IActionResult> Pay(
        [FromQuery] int subscriptionId, CancellationToken cancellationToken)
    {
        var userId = _currentUserHelper.UserId;

        var subscription = await _db.Subscriptions
            .FirstOrDefaultAsync(s => s.Id == subscriptionId && s.UserId == userId);

        if (subscription == null)
            return NotFound();

        if (subscription.Status != SubscriptionStatus.Unpaid)
            return BadRequest("status invalid");

        var paymentId = Guid.NewGuid().ToString();

        subscription.Status = SubscriptionStatus.SentToIpg;
        subscription.PaymentId = paymentId;

        await _db.SaveChangesAsync(cancellationToken);

        return Ok(new { paymentId = paymentId });
    }

    [HttpGet("Verify")]
    public async Task<ActionResult> Verify([FromQuery] string paymentId, CancellationToken cancellationToken)
    {
        var subscription = await _db.Subscriptions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.PaymentId == paymentId);

        if (subscription == null)
            return NotFound();

        if (subscription.Status != SubscriptionStatus.SentToIpg || subscription.CreatedAt < DateTimeOffset.Now.AddMinutes(-1 * 5))
            return BadRequest("Bad condition, can't verify");

        subscription.Status = SubscriptionStatus.Verified;
        subscription.PaidAt = DateTimeOffset.Now;
        subscription.User.SearchCount += subscription.SearchCount;

        await _db.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}