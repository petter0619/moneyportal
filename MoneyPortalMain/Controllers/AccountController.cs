using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MoneyPortalMain.DTOs;
using MoneyPortalMain.Models;
using System.Text.RegularExpressions;
using MoneyPortalMain.Services;

public class AccountController : Controller
{
    private readonly IConfiguration _config;
    private readonly IUserService _userService;
    private readonly HttpClient _client;

    public AccountController(
        IConfiguration config, 
        IHttpClientFactory clientFactory,
        IUserService userService
    )
    {
        _config = config;
        _userService = userService;
        _client = clientFactory.CreateClient("Auth0");
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
        var values = new Dictionary<string, string>
        {
            { "client_id", _config.GetValue<string>("Auth0:ClientId") },
            { "connection", "Username-Password-Authentication" },
            
            { "email", registrationInfo.Email },
            { "password", registrationInfo.Password },
            { "given_name", registrationInfo.FirstName },
            { "family_name", registrationInfo.LastName },
            { "name", $"{registrationInfo.FirstName} {registrationInfo.LastName}" },
            { "nickname", registrationInfo.Displayname },
        };

        var content = new FormUrlEncodedContent(values);
        var response = await _client.PostAsync("/dbconnections/signup", content);
        
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadFromJsonAsync<Auth0User>();

        // Add user to database
        var createdUsers = _userService.AddNewUser(responseBody._id);

        return Ok(new { 
            NewUsers = createdUsers
        });
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