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

    [HttpGet("GetDescription")]
    public async Task<IActionResult> GetAdDescription(string serviceName, string url)
    {
        var parser = _parserFactory.GetParser(serviceName);

        var description = await parser.GetJobDescription(url);

        return Ok(description);
    }
}