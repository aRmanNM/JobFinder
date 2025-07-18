using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobFinder.Models;
using JobFinder.Parsers;

namespace JobFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IParserFactory _parserFactory;

        public HomeController(ILogger<HomeController> logger, IParserFactory parserFactory)
        {
            _logger = logger;
            _parserFactory = parserFactory;
        }

        public IActionResult Index()
        {
            QueryUrl url = new QueryUrl();
            return View(url);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult JobinjaResult(QueryUrl url)
        {
            _logger.LogInformation("jobinja result request. query: {query}", url.SearchString);

            ViewData["website"] = "Jobinja";
            ViewData["website-name"] = "جابینجا";
            return PartialView("_adPartial", _parserFactory.GetParser("Jobinja").GetJobAds(url));
        }

        public IActionResult QueraResult(QueryUrl url)
        {
            _logger.LogInformation("quera result request. query: {query}", url.SearchString);

            ViewData["website"] = "Quera";
            ViewData["website-name"] = "کوئرا";
            return PartialView("_adPartial", _parserFactory.GetParser("Quera").GetJobAds(url));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
