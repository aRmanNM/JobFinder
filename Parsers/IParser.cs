using System.Collections.Generic;
using System.Threading.Tasks;
using JobFinder.Models;

namespace JobFinder.Parsers
{
    public interface IParser
    {
        string Name { get; }
        List<JobAd> GetJobAds(QueryUrl url);
    }
}