using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace JobFinder.Helpers
{
    public class WebHelper
    {
        public static HtmlDocument? GetHtmlDoc(string queryUrl)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            HtmlWeb web = new HtmlWeb();

            web.UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:128.0) Gecko/20100101 Firefox/128.0";
            web.UsingCache = false;
            web.UseCookies = true;

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

            var task = Task.Run(() => web.Load(queryUrl));

            try
            {
                task.Wait(cts.Token);
                var doc = task.Result;

                return doc;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}