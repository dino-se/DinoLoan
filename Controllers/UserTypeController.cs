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
    public class UserTypeController : Controller
    {
        private readonly DotnetWebMvcContext _context;

        public UserTypeController(DotnetWebMvcContext user)
        {
            _context = user;
        }

        public IActionResult Index()
        {
            var userType = _context.Usertypes.ToList();
            return View(userType);
        }

        [HttpPost]
        public ActionResult AddUserType(string name)
        {
            var userType = new Usertype { Name = name };
            _context.Usertypes.Add(userType);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_context.Usertypes.Where(q=> q.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateUserType(int id, string name)
        {
            var userType = _context.Usertypes.Find(id);
            userType.Name = name;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var userType = _context.Usertypes.Where( q => q.Id == id).FirstOrDefault();
            _context.Usertypes.Remove(userType);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}