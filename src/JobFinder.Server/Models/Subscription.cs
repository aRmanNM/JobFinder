
namespace JobFinder.Server.Models;

public class Subscription
{
    public int Id { get; set; }
    public int SearchCount { get; set; }
    public string? PaymentId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? PaidAt { get; set; }
    public SubscriptionStatus Status { get; set; }

    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;
}

public enum SubscriptionStatus
{
    Unpaid,
    SentToIpg,
    Verified,
}