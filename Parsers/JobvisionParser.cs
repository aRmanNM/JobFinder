using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using JobFinder.Helpers;
using JobFinder.Models;
using Newtonsoft.Json.Linq;

namespace JobFinder.Parsers
{
    public class JobvisionParser : IParser
    {
        private readonly WebHelper _webHelper;

        public JobvisionParser(WebHelper webHelper)
        {
            _webHelper = webHelper;
        }

        public string Name => "Jobvision";

        public async Task<List<JobAd>> GetJobAds(QueryUrl url)
        {
            List<JobAd> jobAds = new List<JobAd>();

            string queryUrl = $"https://candidateapi.jobvision.ir/api/v1/JobPost/List";

            var res = await _webHelper.Post<JobvisionListResponse>(queryUrl, new
            {
                Keyword = url.SearchString,
                RequestedPage = url.PageNumber,
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

        public async Task<string> GetJobDescription(string url)
        {
            var doc = await _webHelper.GetHtmlDoc(url);

            if (doc == null)
                return string.Empty;

            var node = doc.DocumentNode.QuerySelector(".job-specification > div:nth-child(2)");

            if (node == null)
                return string.Empty;

            return node.InnerHtml;
        }
    }
}

public class JobvisionListResponse : BaseWebHelperResponse
{
    public JobvisionListData? data { get; set; }
}

public class JobvisionListData
{
    public List<JobvisionJobPost> jobPosts { get; set; } = new List<JobvisionJobPost>();
}

public class JobvisionJobPost
{
    public string? title { get; set; }
    public string? id { get; set; }
    public JobvisionCompany? company { get; set; }
    public JobvisionActivationTime? activationTime { get; set; }
}

public class JobvisionActivationTime
{
    public string? beautifyFa { get; set; }
}

public class JobvisionCompany
{
    public string? logoFileId { get; set; }
    public string? logoUrl { get; set; }
    public string? nameFa { get; set; }
    public string? MyProperty { get; set; }
    public JobvisionCompanyLocation? location { get; set; }
}

public class JobvisionCompanyLocation
{
    public JobvisionCompanyLocationProvince? province { get; set; }
}

public class JobvisionCompanyLocationProvince
{
    public string? titleFa { get; set; }
}
