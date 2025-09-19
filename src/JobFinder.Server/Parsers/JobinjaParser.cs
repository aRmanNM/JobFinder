using HtmlAgilityPack;
using System.Web;
using HtmlAgilityPack.CssSelectors.NetCore;
using JobFinder.Server.Helpers;
using JobFinder.Server.Models;

namespace JobFinder.Server.Parsers;

public class JobinjaParser : IParser
{
    private readonly WebHelper _webHelper;

    public JobinjaParser(WebHelper webHelper)
    {
        _webHelper = webHelper;
    }

    public string Name => "Jobinja";

    public async Task<List<JobAd>> GetJobAds(string query, int pageNumber)
    {
        List<JobAd> jobAds = new List<JobAd>();

        query = HttpUtility.UrlEncode(query);
        string queryUrl = $"https://jobinja.ir/jobs?filters[keywords][]=&filters[keywords][0]={query}&page={pageNumber}";

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

    public async Task<JobAdDetail> GetJobAdDetail(string url)
    {
        var doc = await _webHelper.GetHtmlDoc(url);

        if (doc == null)
            return new JobAdDetail();

        var node = doc.DocumentNode.QuerySelector("div.o-box__text:nth-child(4)");

        if (node == null)
            return new JobAdDetail();

        return new JobAdDetail { Description = node.InnerHtml };
    }
}