using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace JobFinder.Helpers
{
    public class WebHelper
    {
        private readonly HttpClient _client;

        public WebHelper(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("Default");
        }

        public async Task<HtmlDocument?> GetHtmlDoc(string queryUrl)
        {
            var response = await _client.GetAsync(queryUrl);

            var htmlContent = await response.Content.ReadAsStringAsync();

            var page = new HtmlDocument();
            page.LoadHtml(htmlContent);

            return page;
        }
    }
}