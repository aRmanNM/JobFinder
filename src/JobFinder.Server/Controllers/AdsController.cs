using JobFinder.Server.Helpers;
using JobFinder.Server.Models;
using JobFinder.Server.Parsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AdsController : ControllerBase
{
    private readonly ILogger<AdsController> _logger;
    private readonly IParserFactory _parserFactory;
    private readonly UserManager<AppUser> _userManager;
    private readonly CurrentUserHelper _currentUserHelper;

    public AdsController(
        ILogger<AdsController> logger,
        IParserFactory parserFactory,
        UserManager<AppUser> userManager,
        CurrentUserHelper currentUserHelper)
    {
        _logger = logger;
        _parserFactory = parserFactory;
        _userManager = userManager;
        _currentUserHelper = currentUserHelper;
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetAds(string serviceName, string query, int pageNumber)
    {
        if (string.IsNullOrEmpty(query))
            return Ok(new List<JobAd>());

        await ValidateSearchCount();

        var parser = _parserFactory.GetParser(serviceName);

        var ads = await parser.GetJobAds(query, pageNumber);

        ads.ForEach(ad =>
        {
            ad.Id = IdHelper.GetId(ad.Url);
            ad.ServiceName = serviceName;
        });

        await ProcessSearchCount();

        return Ok(ads);
    }

    [HttpGet("GetDetail")]
    public async Task<IActionResult> GetAdDetail(string serviceName, string url)
    {
        var parser = _parserFactory.GetParser(serviceName);

        var adDetail = await parser.GetJobAdDetail(url);

        return Ok(adDetail);
    }

    private async Task ValidateSearchCount()
    {
        var userId = _currentUserHelper.UserId;

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new Exception("user not found");

        if (user.SearchCount == 0)
            throw new Exception("user search count is zero");
    }

    private async Task ProcessSearchCount()
    {
        var userId = _currentUserHelper.UserId;

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new Exception("user not found");

        user.SearchCount--;

        await _userManager.UpdateAsync(user);
    }
}