namespace JobFinder.Server.Models;

public class SourceAds
{
    public string ServiceName { get; set; } = null!;
    public List<JobAd> Ads { get; set; } = new List<JobAd>();
    public int PageNumber { get; set; }
}