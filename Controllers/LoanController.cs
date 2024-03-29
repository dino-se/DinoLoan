using Microsoft.AspNetCore.Mvc;

namespace dotnet_web_mvc.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
