using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DinoLoan.Entity;
using DinoLoan.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DinoLoan.Controllers
{
    public class ClientController : Controller
    {
        private readonly DinoloanDbContext _context;

        public ClientController(DinoloanDbContext client)
        {
            _context = client;
        }

       public IActionResult Index()
        {
            var clientInfos = (
                from clientInfo in _context.Clientinfos
                join usertype in _context.Usertypes
                on clientInfo.UserType equals usertype.Id
                select new ClientInfoViewModel
                {
                    Id = clientInfo.Id,
                    UserType = clientInfo.UserType,
                    UserTypeName = usertype.Name,
                    FirstName = clientInfo.FirstName,
                    MiddleName = clientInfo.MiddleName,
                    LastName = clientInfo.LastName,
                    Address = clientInfo.Address,
                    ZipCode = clientInfo.ZipCode,
                    Birthday = clientInfo.Birthday,
                    Age = clientInfo.Age,
                    NameOfFather = clientInfo.NameOfFather,
                    NameOfMother = clientInfo.NameOfMother,
                    CivilStatus = clientInfo.CivilStatus,
                    Religion = clientInfo.Religion,
                    Occupation = clientInfo.Occupation,
                }).ToList();
            return View(clientInfos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientinfos == null)
            {
                return NotFound();
            }

            var clientInfos = await _context.Clientinfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientInfos == null)
            {
                return NotFound();
            }

            return View(clientInfos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userTypes = _context.Usertypes.ToList();
            var clientInfo = new Clientinfo();
            ViewData["UserTypes"] = userTypes;
            return View(clientInfo);
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

            if (client == null)
            {
                return NotFound();
            }
            _context.Clientinfos.Remove(client);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}