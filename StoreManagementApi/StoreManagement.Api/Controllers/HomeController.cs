using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}