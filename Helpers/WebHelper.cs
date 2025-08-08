using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using JobFinder.Models;
using Newtonsoft.Json;

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

        public async Task<T?> Post<T>(string url, object body) where T : BaseWebHelperResponse
        {
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T?>(result);
        }
    }
}