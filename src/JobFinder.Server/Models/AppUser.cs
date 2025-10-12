using Microsoft.AspNetCore.Identity;

namespace JobFinder.Server.Models;

public class AppUser : IdentityUser<string>
{
    public string? PictureUid { get; set; }
    public DateTimeOffset JoinedAt { get; set; }
    public int SearchCount { get; set; }
    public List<string> RecentQueries { get; set; } = new List<string>();
    public List<string> Tags { get; set; } = new List<string>();
}