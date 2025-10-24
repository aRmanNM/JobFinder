using Microsoft.AspNetCore.Identity;

namespace JobFinder.Server.Models;

public class AppUser : IdentityUser
{
    public string? PictureUid { get; set; }
    public DateTimeOffset JoinedAt { get; set; }
    public int SearchCount { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public List<RecentQuery> RecentQueries { get; set; } = new List<RecentQuery>();
}