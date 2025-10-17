namespace JobFinder.Server.Models;

public class RecentQuery
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string Query { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}