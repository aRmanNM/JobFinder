namespace JobFinder.Server.Models;

public class ProfileModel
{
    public string? Username { get; set; }
    public string? PictureUid { get; set; }
    public DateTimeOffset JoinedAt { get; set; }
    public int SearchCount { get; set; }
    public List<RecentQuery> RecentQueries { get; set; } = new List<RecentQuery>();
    public List<string> Tags { get; set; } = new List<string>();

    public static ProfileModel MapFromAppUser(AppUser user) => new ProfileModel
    {
        Username = user.UserName,
        PictureUid = user.PictureUid,
        JoinedAt = user.JoinedAt,
        SearchCount = user.SearchCount,
        RecentQueries = user.RecentQueries,
        Tags = user.Tags,
    };
}