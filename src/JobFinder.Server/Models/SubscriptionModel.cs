namespace JobFinder.Server.Models;

public class SubscriptionModel
{
    public int Id { get; set; }
    public int SearchCount { get; set; }
    public DateTimeOffset? PaidAt { get; set; }
    public SubscriptionStatus Status { get; set; }

    public static SubscriptionModel MapFromSubscription(Subscription subscription) => new SubscriptionModel
    {
        Id = subscription.Id,
        SearchCount = subscription.SearchCount,
        PaidAt = subscription.PaidAt,
        Status = subscription.Status,
    };
}