using Microsoft.AspNetCore.Mvc;

namespace MoneyPortalMain.Controllers
{
    public class BudgetsController : Controller
    {
        [HttpGet("[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[controller]/[action]")]
        public IActionResult Details([FromQuery(Name = "id")] int budgetId)
        {
            if (budgetId == 0)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost("[controller]/[action]")]
        public IActionResult Create()
        {
            return Ok("Create new budget");
        }
    }
}
