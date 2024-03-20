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

        [HttpPost]
        public IActionResult Create(Clientinfo c)
        {
            _context.Clientinfos.Add(c);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var client = _context.Clientinfos.FirstOrDefault(q => q.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            var userTypes = _context.Usertypes.ToList();
            var selectedUserType = client.UserType;

            ViewData["UserTypes"] = userTypes;
            ViewData["SelectedUserType"] = selectedUserType;

            return View(client);
        }

        [HttpPost]
        public IActionResult Update(Clientinfo c)
        {
            _context.Clientinfos.Update(c);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var client = _context.Clientinfos.Where(q => q.Id == id).FirstOrDefault();
            _context.Clientinfos.Remove(client);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}