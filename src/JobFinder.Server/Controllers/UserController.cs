using JobFinder.Server.Helpers;
using JobFinder.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Server.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly CurrentUserHelper _currentUserHelper;

    public UserController(
        UserManager<AppUser> userManager,
        CurrentUserHelper currentUserHelper)
    {
        _userManager = userManager;
        _currentUserHelper = currentUserHelper;
    }

    [HttpPut("Profile")]
    public async Task<IActionResult> UpdateProfile(ProfileModel model)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.Id == _currentUserHelper.UserId);

        if (user == null)
            return NotFound();

        user.PictureUid = model.PictureUid;
        user.Tags = model.Tags;

        await _userManager.UpdateAsync(user);
        return Ok(ProfileModel.MapFromAppUser(user));
    }

    [HttpGet("Profile")]
    public async Task<IActionResult> GetProfile()
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Include(u => u.RecentQueries)
            .FirstOrDefaultAsync(u => u.Id == _currentUserHelper.UserId);

        if (user == null)
            return NotFound();

        return Ok(ProfileModel.MapFromAppUser(user));
    }
}