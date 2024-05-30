using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DinoLoan.Entity;
using DinoLoan.ViewModels;

namespace DinoLoan.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly DinoloanDbContext _context;

        public UserTypeController(DinoloanDbContext user)
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
            return View(_context.Usertypes.Where(q=> q.Id == id).Select(q => new UserTypeViewModel {
                Name = q.Name,
            }).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateUserType(int id, UserTypeViewModel name)
        {
            if(ModelState.IsValid)
            {
                var userType = _context.Usertypes.Find(id);
                userType.Name = name.Name;
                _context.SaveChanges();
            }

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