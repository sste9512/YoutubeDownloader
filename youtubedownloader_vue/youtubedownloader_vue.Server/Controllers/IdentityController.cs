using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace youtubedownloader_vue.Server.Controllers;

public sealed class IdentityController(ApplicationIdentityDbContext context, SignInManager<ApplicationUser> signInManager) : ApiControllerBase
{
    [HttpGet("[action]")]
    public async Task<ActionResult<bool>> IsAuthenticated()
    {
        return User?.Identity?.IsAuthenticated ?? false;
    }

    [HttpGet("[action]")]
    [HttpPost("[action]")]
    public async Task<ActionResult<string>> Login([FromBody] LoginRequest request)
    {
        var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: true, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return BadRequest("Invalid username or password");
        }

        return Ok(request.Email);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<string?>> GetUsername()
    {
        return User?.Identity?.Name;
    }
}
