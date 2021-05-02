using System.Collections.Generic;
using JobFinder.Models;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;


namespace JobFinder.Services
{
    public class JobinjaParser : IParser
    {
        public List<JobAd> GetJobAds(QueryUrl url)
        {
            List<JobAd> JobAds = new List<JobAd>();

            string queryUrl = $"https://jobinja.ir/jobs?filters%5Bkeywords%5D%5B%5D=&filters%5Blocations%5D%5B%5D=%D8%AA%D9%87%D8%B1%D8%A7%D9%86&filters%5Bjob_categories%5D%5B%5D=%D9%88%D8%A8%D8%8C%E2%80%8C+%D8%A8%D8%B1%D9%86%D8%A7%D9%85%D9%87%E2%80%8C%D9%86%D9%88%DB%8C%D8%B3%DB%8C+%D9%88+%D9%86%D8%B1%D9%85%E2%80%8C%D8%A7%D9%81%D8%B2%D8%A7%D8%B1&filters[keywords][0]={url.SearchString}&page={url.PageNumber}";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(queryUrl);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//*[@id=\"js-jobSeekerSearchResult\"]/div/div[2]/section/div/ul/li");

            foreach (var node in nodes)
            {
                JobAds.Add(new JobAd(){
                    Title = node.SelectSingleNode(".//div/div[1]/h3/a").InnerHtml.CleanStr(),
                    LogoUrl = node.SelectSingleNode(".//div/div[1]/a/img").Attributes["src"].Value,
                    Company = node.SelectSingleNode(".//div/div[1]/ul/li[1]/span").InnerHtml.CleanStr(),
                    Location = node.SelectSingleNode(".//div/div[1]/ul/li[2]/span").InnerHtml.CleanStr(),
                    Url = node.SelectSingleNode(".//div/div[1]/h3/a").Attributes["href"].Value
                });
            }

            return JobAds;
        }
    }
}