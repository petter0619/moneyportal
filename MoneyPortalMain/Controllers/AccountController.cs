using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MoneyPortalMain.DTOs;
using MoneyPortalMain.Services;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Account/Login")]
    public async Task Login(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
          // Indicate here where Auth0 should redirect the user after a login.
          // Note that the resulting absolute Uri must be added to the
          // **Allowed Callback URLs** settings for the app.
          .WithRedirectUri(returnUrl)
          .Build();

        await HttpContext.ChallengeAsync(
          Auth0Constants.AuthenticationScheme,
          authenticationProperties
        );
    }

    [Authorize]
    [HttpGet("Account/Logout")]
    public async Task Logout()
    {
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
          // Indicate here where Auth0 should redirect the user after a logout.
          // Note that the resulting absolute Uri must be added to the
          // **Allowed Logout URLs** settings for the app.
          .WithRedirectUri(Url.Action("Index", "Home"))
          .Build();

        // Logout from Auth0
        await HttpContext.SignOutAsync(
          Auth0Constants.AuthenticationScheme,
          authenticationProperties
        );
        // Logout from the application
        await HttpContext.SignOutAsync(
          CookieAuthenticationDefaults.AuthenticationScheme
        );
    }
    
    [HttpGet("Account/Register")]
    public async Task<IActionResult> Register()
    {
        var model = new RegisterDto() 
        {
            Email = "bob@test.com",
            Password = "Pa$$w0rd!",
            FirstName = "Bob",
            LastName = "Bobertson",
            Displayname = "Bobert"
        };

        return View(model);
    }
    
    [HttpPost("Account/Register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto registrationInfo)
    {
        var createdUsers = await _userService.AddNewUser(registrationInfo);

        return RedirectToAction("Login");
    }

    [Authorize]
    [HttpPost("Account/ChangePassword")]
    public async Task<IActionResult> ChangePassword()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        await _userService.RequestUserPasswordChange(userId);

        return Ok();
    }

    [Authorize]
    [HttpGet("Account/Profile")]
    public IActionResult Profile()
    {
        return Ok(new
        {
            Name = User.Identity.Name,
            NameIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
            UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.Replace("auth0|", ""),
            ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
            NickName = User.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value
        });
    }
}