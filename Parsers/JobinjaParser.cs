using System.Collections.Generic;
using JobFinder.Models;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System;
using JobFinder.Helpers;
using System.Web;
using System.IO;


namespace JobFinder.Parsers
{
    public class JobinjaParser : IParser
    {
        public string Name => "Jobinja";

        public List<JobAd> GetJobAds(QueryUrl url)
        {
            List<JobAd> jobAds = new List<JobAd>();

            url.SearchString = HttpUtility.UrlEncode(url.SearchString);
            string queryUrl = $"https://jobinja.ir/jobs?filters[keywords][]=&filters[keywords][0]={url.SearchString}&page={url.PageNumber}";

            var doc = WebHelper.GetHtmlDoc(queryUrl);

            if (doc == null)
                return new List<JobAd>();

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//*[@id=\"js-jobSeekerSearchResult\"]/div/div[2]/section/div/ul/li");

            if (nodes == null)
                return new List<JobAd>();

            foreach (var node in nodes)
            {
                if (node == null)
                    continue;

                jobAds.Add(new JobAd()
                {
                    Title = node.SelectSingleNode(".//div/div[1]/h2/a")?.InnerHtml?.CleanStr(),
                    LogoUrl = node.SelectSingleNode(".//div/div[1]/a/img")?.Attributes["src"]?.Value,
                    Company = node.SelectSingleNode(".//div/div[1]/ul/li[1]/span")?.InnerHtml?.CleanStr(),
                    Location = node.SelectSingleNode(".//div/div[1]/ul/li[2]/span")?.InnerHtml?.CleanStr(),
                    Url = node.SelectSingleNode(".//div/div[1]/h2/a")?.Attributes["href"]?.Value,
                    DateIdentifier = node.SelectSingleNode(".//div/div[1]/h2/span")?.InnerHtml?.CleanStr(),
                });
            }

            return jobAds;
        }
    }
}