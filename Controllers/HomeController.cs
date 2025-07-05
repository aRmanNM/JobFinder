using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobFinder.Models;
using JobFinder.Services;

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

        public async Task<IActionResult> JobinjaResult(QueryUrl url)
        {
            ViewData["website"] = "Jobinja";
            ViewData["website-name"] = "جابینجا";
            return PartialView("_adPartial", await _parserFactory.GetParser("Jobinja").GetJobAds(url));
        }

        public async Task<IActionResult> QueraResult(QueryUrl url)
        {
            ViewData["website"] = "Quera";
            ViewData["website-name"] = "کوئرا";
            return PartialView("_adPartial", await _parserFactory.GetParser("Quera").GetJobAds(url));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
