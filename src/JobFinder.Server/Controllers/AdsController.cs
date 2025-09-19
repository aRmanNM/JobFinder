using JobFinder.Server.Parsers;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AdsController : ControllerBase
{
    private readonly ILogger<AdsController> _logger;
    private readonly IParserFactory _parserFactory;

    public AdsController(
        ILogger<AdsController> logger, IParserFactory parserFactory)
    {
        _logger = logger;
        _parserFactory = parserFactory;
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetAds(string serviceName, string query, int pageNumber)
    {
        var parser = _parserFactory.GetParser(serviceName);

        var ads = await parser.GetJobAds(query, pageNumber);

        return Ok(ads);
    }

    [HttpGet("GetDetail")]
    public async Task<IActionResult> GetAdDetail(string serviceName, string url)
    {
        var parser = _parserFactory.GetParser(serviceName);

        var adDetail = await parser.GetJobAdDetail(url);

        return Ok(adDetail);
    }
}