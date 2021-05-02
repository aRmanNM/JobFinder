using System.Collections.Generic;
using JobFinder.Models;

namespace JobFinder.Services
{
    public class FakeQueraParser : IParser
    {
        public List<JobAd> GetJobAds(QueryUrl url)
        {
            List<JobAd> JobAds = new List<JobAd>();

            JobAds.Add(new JobAd(){
                Title = url.SearchString,
                Url = "www.google.com",
                Description = "Description Here",
                Company = "company name",
                LogoUrl = "/img/logo.png",
                Location = url.Location,
                Contract = new string[]{"fulltime", "parttime"},
                Experience = "experience here",
                Date = "date added",
                Abilities = new string[]{"c#", "sql", "javascript"}
            });
            return JobAds;
        }
    }
}