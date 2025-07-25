using System.Collections.Generic;
using System.Threading.Tasks;
using JobFinder.Models;

namespace JobFinder.Parsers
{
    public interface IParser
    {
        string Name { get; }
        Task<List<JobAd>> GetJobAds(QueryUrl url);
        Task<string> GetJobDescription(string url);
    }
}