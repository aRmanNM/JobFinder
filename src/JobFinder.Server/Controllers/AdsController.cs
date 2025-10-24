using JobFinder.Server.Helpers;
using JobFinder.Server.Models;
using JobFinder.Server.Parsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AdsController : ControllerBase
{
    private readonly ILogger<AdsController> _logger;
    private readonly IParserFactory _parserFactory;
    private readonly CurrentUserHelper _currentUserHelper;
    private readonly UserManager<AppUser> _userManager;

    public AdsController(
        ILogger<AdsController> logger,
        IParserFactory parserFactory,
        CurrentUserHelper currentUserHelper,
        UserManager<AppUser> userManager)
    {
        _logger = logger;
        _parserFactory = parserFactory;
        _currentUserHelper = currentUserHelper;
        _userManager = userManager;
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetAds([FromQuery] List<string> serviceNames, string query, int pageNumber)
    {
        if (string.IsNullOrEmpty(query))
            return Ok(new List<JobAd>());

        var res = new List<SourceAds>();
        foreach (var serviceName in serviceNames)
        {
            var parser = _parserFactory.GetParser(serviceName);

            var ads = await parser.GetJobAds(query, pageNumber);

            ads.ForEach(ad =>
            {
                ad.Id = IdHelper.GetId(ad.Url);
                ad.ServiceName = serviceName;
            });

            res.Add(new SourceAds
            {
                Ads = ads,
                ServiceName = serviceName,
                PageNumber = pageNumber,
            });
        }

        var userId = _currentUserHelper.UserId;
        Console.WriteLine($"**** {userId}");
        if (!string.IsNullOrEmpty(userId))
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.SearchCount++;
                user.RecentQueries.Add(new RecentQuery
                {
                    UserId = userId,
                    Query = query,
                    CreatedAt = DateTimeOffset.Now,
                });

                await _userManager.UpdateAsync(user);
            }
        }

        return Ok(res);
    }

    [HttpGet("GetDetail")]
    public async Task<IActionResult> GetAdDetail(string serviceName, string url)
    {
        var parser = _parserFactory.GetParser(serviceName);

        var adDetail = await parser.GetJobAdDetail(url);

        return Ok(adDetail);
    }
}