namespace JobFinder.Server.Models;

public class JobAd
{
    public string? Title { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    public string? Company { get; set; }
    public string? LogoUrl { get; set; }
    public string? Location { get; set; }
    public List<string> Contract { get; set; } = new List<string>();
    public string? Experience { get; set; }
    public string? DateIdentifier { get; set; } // this is not necessarily a date string
    public List<string> Abilities { get; set; } = new List<string>();
}