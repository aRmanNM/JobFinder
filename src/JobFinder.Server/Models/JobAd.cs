namespace JobFinder.Server.Models;

public class JobAd
{
    public string Id { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
    public string? Title { get; set; }
    public string Url { get; set; } = null!;
    public string? Company { get; set; }
    public string? LogoUrl { get; set; }
    public string? Location { get; set; }
    public List<string> Contract { get; set; } = new List<string>();
    public string? Experience { get; set; }
    public string? DateIdentifier { get; set; } // this is not necessarily a date string
    public List<string> Abilities { get; set; } = new List<string>();
}