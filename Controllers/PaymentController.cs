using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DinoLoan.Entity;
using DinoLoan.Models;
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

            ViewData["ClientId"] = payment.FirstOrDefault()?.ClientId;

            return View(payment);
        }

        [HttpPost]
        public IActionResult Index(PaymentViewModel pvm)
        {
            Loan? loan = _context.Loans.FirstOrDefault(l => l.Id == pvm.Lid);
            Payment? payment = _context.Payments.FirstOrDefault(l => l.PaymentId == pvm.Pid);

            if (loan == null || payment == null)
            {
                return NotFound();
            }

            loan.Collected += pvm.Amnt;
            loan.Collectable = Math.Max(loan.Collectable - pvm.Amnt, 0);
            payment.Collectable = Math.Max(payment.Collectable - pvm.Amnt, 0);

            _context.Loans.Update(loan);
            _context.Payments.Update(payment);
            _context.SaveChanges();

            LogTransaction(pvm);

            return RedirectToAction("Index", new { id = pvm.Lid });
        }

        private void LogTransaction(PaymentViewModel pvm) {
            var transaction = new Transaction
            {
                LoanId = pvm.Lid,
                Amount = pvm.Amnt
            };

            _context.Transactions.Add(transaction);
           _context.SaveChanges();
        }

    }
}