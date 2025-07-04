using System.Collections.Generic;
using JobFinder.Models;

namespace JobFinder.Services
{
    public class FakeJobinjaParser : IParser
    {
        public List<JobAd> GetJobAds(QueryUrl url)
        {
            List<JobAd> JobAds = new List<JobAd>();

            JobAds.Add(new JobAd(){
                Title = url.SearchString,
                Url = "www.google.com",
                Description = "Description Here",
                Company = "company name",
                LogoUrl ="/img/logo.png",
                Location = url.Location,
                Contract = new List<string>() {"fulltime", "parttime"},
                Experience = "experience here",
                Date = "date added",
                Abilities = new List<string>() {"c#", "sql", "javascript"}
            });

            JobAds.Add(new JobAd(){
                Title = "job title2",
                Url = "www.google.com",
                Description = "Description Here2",
                Company = "company name2",
                LogoUrl = "/img/logo.png",
                Location = url.Location,
                Contract = new List<string>() {"fulltime"},
                Experience = "2 years",
                Date = "today",
                Abilities = new List<string>() {"python", "djngo", "nosql"}
            });

            return JobAds;
        }
    }
}