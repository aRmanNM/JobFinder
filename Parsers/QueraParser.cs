using System.Collections.Generic;
using JobFinder.Models;
using HtmlAgilityPack;
using JobFinder.Helpers;
using System.Web;
using System.Threading.Tasks;

namespace JobFinder.Parsers
{
    public class QueraParser : IParser
    {
        private readonly WebHelper _webHelper;

        public QueraParser(WebHelper webHelper)
        {
            _webHelper = webHelper;
        }

        public string Name => "Quera";

        public async Task<List<JobAd>> GetJobAds(QueryUrl url)
        {
            List<JobAd> jobAds = new List<JobAd>();

            url.SearchString = HttpUtility.UrlEncode(url.SearchString);
            string queryUrl = $"https://quera.org/magnet/jobs?search={url.SearchString}&page={url.PageNumber}";

            var doc = await _webHelper.GetHtmlDoc(queryUrl);

            if (doc == null)
                return new List<JobAd>();

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("/html/body/div[6]/div[2]/main/section/div[2]/article");

            if (nodes == null)
                return new List<JobAd>();

            foreach (var node in nodes)
            {
                if (node == null)
                    continue;

                jobAds.Add(new JobAd()
                {
                    Title = node.SelectSingleNode(".//div/div/div[2]/div[1]/h2/a/span")?.InnerHtml?.CleanStr(),
                    LogoUrl = "https://quera.ir" + node.SelectSingleNode(".//div/div/div[1]/a/div/img")?.Attributes["src"]?.Value,
                    Company = node.SelectSingleNode(".//div/div/div[2]/div[2]/div/p")?.InnerHtml?.CleanStr(),
                    Location = node.SelectSingleNode(".//div/div/div[2]/div[2]/div[2]/span")?.InnerHtml?.CleanStr(),
                    Url = "https://quera.ir" + node.SelectSingleNode(".//div/div/div[2]/div[1]/h2/a")?.Attributes["href"]?.Value,
                    DateIdentifier = node.SelectSingleNode(".//div/div/div[2]/div[1]/div[2]/span")?.InnerHtml?.CleanStr(),
                });
            }

            return jobAds;
        }

        public async Task<string> GetJobDescription(string url)
        {
            return await Task.FromResult(string.Empty);
        }
    }
}