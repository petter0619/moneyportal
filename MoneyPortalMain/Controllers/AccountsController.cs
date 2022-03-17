using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoneyPortalMain.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        [HttpGet("[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("BankAccount/[action]")]
        [HttpGet("Cash/[action]")]
        [HttpGet("CreditCard/[action]")]
        public IActionResult Details([FromQuery(Name = "id")] int accountId)
        {
            if (accountId == 0)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult Create()
        {
            return Ok("Create new savings/checking/cash/credit card account");
        }
    }
}
