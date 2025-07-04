using System.Collections.Generic;
using JobFinder.Models;

namespace JobFinder.Services
{
    public interface IParser
    {
        public string Name { get; }
        public List<JobAd> GetJobAds(QueryUrl url);
    }
}