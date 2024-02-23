using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_web_mvc.Models;

namespace dotnet_web_mvc.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult MethodWithParams(int a, int b)
    {
        int c = a * b;

        ViewBag.MyValue = c;
        return View();
    }

    // Useless
    
    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
