using JobFinder.Server.Models;

namespace JobFinder.Server.Parsers;

public interface IParser
{
    string Name { get; }
    Task<List<JobAd>> GetJobAds(string query, int pageNumber);
    Task<string> GetJobDescription(string url);
}