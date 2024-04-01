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
    public class LibraryController : Controller
    {
        private readonly DinoloanDbContext _context;

        public LibraryController(DinoloanDbContext library)
        {
            _context = library;
        }

        public IActionResult Index()
        {
            var book = _context.Books.ToList();
            return View(book);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book b)
        {
            _context.Books.Add(b);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_context.Books.Where(q=> q.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var book = _context.Books.Where( q => q.Id == id).FirstOrDefault();
            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}