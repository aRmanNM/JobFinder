using System.Collections.Generic;
using JobFinder.Models;
using HtmlAgilityPack;
using JobFinder.Helpers;
using System.Web;
using System.Threading.Tasks;

namespace JobFinder.Parsers
{
    public class JobinjaParser : IParser
    {
        private readonly WebHelper _webHelper;

        public JobinjaParser(WebHelper webHelper)
        {
            _webHelper = webHelper;
        }

        public string Name => "Jobinja";

        public async Task<List<JobAd>> GetJobAds(QueryUrl url)
        {
            List<JobAd> jobAds = new List<JobAd>();

            url.SearchString = HttpUtility.UrlEncode(url.SearchString);
            string queryUrl = $"https://jobinja.ir/jobs?filters[keywords][]=&filters[keywords][0]={url.SearchString}&page={url.PageNumber}";

            var doc = await _webHelper.GetHtmlDoc(queryUrl);

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

        public async Task<string> GetJobDescription(string url)
        {
            var doc = await _webHelper.GetHtmlDoc(url);

            if (doc == null)
                return string.Empty;

            var nodes = doc.DocumentNode.SelectSingleNode("/html/body/div/div[3]/div[1]/div/div[1]/section/div[2]");

            // if (node == null)
            //     return string.Empty;

            // return node.InnerHtml;

            return string.Empty;
        }
    }
}