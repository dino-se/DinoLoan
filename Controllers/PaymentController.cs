using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DinoLoan.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DinoLoan.Controllers
{
    public class PaymentController : Controller
    {
        private readonly DinoloanDbContext _context;

        public PaymentController(DinoloanDbContext payments)
        {
            _context = payments;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var payment = _context.Payments.Where(e => e.LoanId == id).ToList();
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost]
        public IActionResult Index() {
            return View();
        }
    }
}