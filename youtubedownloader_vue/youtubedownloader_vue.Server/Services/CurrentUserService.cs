using System.Security.Claims;
using CleanArchitecture.Application.Common.Interfaces;

namespace youtubedownloader_vue.Server.Services;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string? UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
