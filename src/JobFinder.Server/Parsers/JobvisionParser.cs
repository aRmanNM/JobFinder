using HtmlAgilityPack.CssSelectors.NetCore;
using JobFinder.Server.Helpers;
using JobFinder.Server.Models;

namespace JobFinder.Server.Parsers;

public class JobvisionParser : IParser
{
    private readonly WebHelper _webHelper;

    public JobvisionParser(WebHelper webHelper)
    {
        _webHelper = webHelper;
    }

    public string Name => "Jobvision";

    public async Task<List<JobAd>> GetJobAds(string query, int pageNumber)
    {
        List<JobAd> jobAds = new List<JobAd>();

        string queryUrl = $"https://candidateapi.jobvision.ir/api/v1/JobPost/List";

        var res = await _webHelper.Post<JobvisionListResponse>(queryUrl, new
        {
            Keyword = query,
            RequestedPage = pageNumber,
            pageSize = 10,
        });

        if (res == null || res.data?.jobPosts == null)
            return new List<JobAd>();

        foreach (var job in res.data.jobPosts)
        {
            if (job == null)
                continue;

            jobAds.Add(new JobAd()
            {
                Title = job.title,
                LogoUrl = $"https://fileapi.jobvision.ir/api/v1.0/files/getimage?fileid={job.company?.logoFileId}&width=100&height=100",
                Company = job.company?.nameFa,
                Location = job.company?.location?.province?.titleFa,
                Url = "https://jobvision.ir/jobs/" + job.id,
                DateIdentifier = job.activationTime?.beautifyFa,
            });
        }

        return jobAds;
    }

    public async Task<JobAdDetail> GetJobAdDetail(string url)
    {
        var doc = await _webHelper.GetHtmlDoc(url);

        if (doc == null)
            return new JobAdDetail();

        var node = doc.DocumentNode.QuerySelector(".job-specification > div:nth-child(2)");

        if (node == null)
            return new JobAdDetail();

        return new JobAdDetail { Description = node.InnerHtml };
    }
}
