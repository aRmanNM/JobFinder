using System.Collections.Generic;
using System.Threading.Tasks;
using JobFinder.Models;

namespace JobFinder.Services
{
    public interface IParser
    {
        string Name { get; }
        Task<List<JobAd>> GetJobAds(QueryUrl url);
    }
}