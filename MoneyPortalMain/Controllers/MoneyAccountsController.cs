using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyPortalMain.Services;

namespace MoneyPortalMain.Controllers
{
    [Authorize]
    public class MoneyAccountsController : Controller
    {
        private readonly IAccountsService _accountsService;

        public MoneyAccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpGet("Accounts")]
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

            var account = _accountsService.GenerateMoneyAccountsDetailsViewModel(accountId);

            if (accountId == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost("Accounts/[action]")]
        public IActionResult Create()
        {
            return Ok("Create new savings/checking/cash/credit card account");
        }
    }
}
