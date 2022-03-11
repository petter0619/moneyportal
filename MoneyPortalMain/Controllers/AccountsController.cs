using Microsoft.AspNetCore.Mvc;

namespace MoneyPortalMain.Controllers
{
    public class AccountsController : Controller
    {
        [HttpGet("[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[controller]/[action]/{accountName}")]
        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult Create()
        {
            return Ok("Create new savings/checking/cash/credit card account");
        }
    }
}
