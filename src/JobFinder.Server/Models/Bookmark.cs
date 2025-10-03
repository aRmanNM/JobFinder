
namespace JobFinder.Server.Models;

public class Bookmark
{
    public int Id { get; set; }
    public string? Note { get; set; }
    public JobAd Content { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? LastEditAt { get; set; }
}