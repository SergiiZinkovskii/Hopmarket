using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public class CompanyController : Controller
    {
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Delivery()
        {
            return View();
        }
    }
}
