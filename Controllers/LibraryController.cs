using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_web_mvc.Controllers
{
    public class LibraryController : Controller
    {

        public LibraryController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}