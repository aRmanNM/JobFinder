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

        public IActionResult JobinjaResult(QueryUrl url)
        {
            ViewData["website"] = "Jobinja";
            return PartialView("_adPartial", _parserFactory.GetParsers()[1].GetJobAds(url));
        }

        public IActionResult QueraResult(QueryUrl url)
        {
            ViewData["website"] = "Quera";
            return PartialView("_adPartial", _parserFactory.GetParsers()[0].GetJobAds(url));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
