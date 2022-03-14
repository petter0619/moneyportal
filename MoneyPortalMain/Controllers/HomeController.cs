using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyPortalMain.DTOs;
using MoneyPortalMain.Models;
using MoneyPortalMain.Services;
using System.Diagnostics;

namespace MoneyPortalMain.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;

        public HomeController(
            ILogger<HomeController> logger, 
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            TokenService tokenService
        )
        {
            _logger = logger;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                Response.Cookies.Append("jwttoken", _tokenService.CreateToken(user));
                return RedirectToAction("Index", "Accounts");
            }

            return Unauthorized();
        }

        [HttpGet("[action]")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("[controller]/[action]")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}