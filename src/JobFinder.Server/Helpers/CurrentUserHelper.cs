using System.Security.Claims;

namespace JobFinder.Server.Helpers;

public class CurrentUserHelper
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserHelper(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string UserId =>
        _contextAccessor.HttpContext?.User?
            .FindFirstValue(ClaimTypes.NameIdentifier)
                ?? string.Empty;
}