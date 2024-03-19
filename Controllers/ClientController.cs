using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnet_web_mvc.Entity;

namespace dotnet_web_mvc.Controllers
{
    public class ClientController : Controller
    {
        private readonly DotnetWebMvcContext _context;

        public ClientController(DotnetWebMvcContext client)
        {
            _context = client;
        }

       public IActionResult Index()
        {
            var userTypes = _context.Usertypes.ToList();
            var clientInfo = _context.Clientinfos.ToList();
            ViewData["UserTypes"] = userTypes;
            return View(clientInfo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userTypes = _context.Usertypes.ToList();
            ViewData["UserTypes"] = userTypes;
            return View();
        }

    }
}