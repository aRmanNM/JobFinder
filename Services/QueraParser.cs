using System.Collections.Generic;
using JobFinder.Models;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;


namespace JobFinder.Services
{
    public class QueraParser : IParser
    {
        public List<JobAd> GetJobAds(QueryUrl url)
        {
            List<JobAd> JobAds = new List<JobAd>();

            string queryUrl = "https://quera.ir/careers/jobs?level=I&city=T";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(queryUrl);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//*[@id=\"jobs-segment\"]/div");

            foreach (var node in nodes)
            {
                JobAds.Add(new JobAd(){
                    Title = node.SelectSingleNode(".//div[2]/h2/a/text()").InnerHtml.CleanStr(),
                    LogoUrl = "https://quera.ir/" + node.SelectSingleNode(".//div[1]/a/img").Attributes["src"].Value,
                    Company = node.SelectNodes(".//div[2]/div[2]")[1].InnerHtml.Split('-')[0],
                    Location = node.SelectNodes(".//div[2]/div[2]")[1].InnerHtml.Split('-')[1],
                    Url = "https://quera.ir/" + node.SelectSingleNode(".//div[2]/h2/a").Attributes["href"].Value
                });
            }

            return JobAds;
        }
    }
}