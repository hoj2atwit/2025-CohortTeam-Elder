using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SmartGym.Data;
using SmartGym.Models;
using SmartGym.Services;

namespace SmartGym.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
  private readonly SignInManager<AppUser> _signIn;

  public AuthController(SignInManager<AppUser> signIn) => _signIn = signIn;

  [HttpPost("login")]
  [IgnoreAntiforgeryToken]
  [AllowAnonymous]
  public async Task<IActionResult> Login([FromForm] LoginDTO req)
  {
    var result = await _signIn.PasswordSignInAsync(req.Email, req.Password, req.RememberMe, lockoutOnFailure: true);
    if (!result.Succeeded)
    {
      return Unauthorized("Invalid credentials");
    }
    // return Ok(new { message = "Logged in" });
    var target = (!string.IsNullOrEmpty(req.ReturnUrl) && Url.IsLocalUrl(req.ReturnUrl))
            ? req.ReturnUrl
            : "/dashboard";
    return LocalRedirect(target);
  }

  [HttpPost("logout")]
  [IgnoreAntiforgeryToken]
  public async Task<IActionResult> Logout()
  {
    await _signIn.SignOutAsync();

    return LocalRedirect("/");
  }
}